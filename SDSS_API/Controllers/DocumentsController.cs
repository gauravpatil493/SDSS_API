using SDSS_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SDSS_API.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/documents")]
    public class DocumentsController : ApiController
    {
        private readonly DocumentDbEntities _db = new DocumentDbEntities();
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("upload")]
        public async Task<IHttpActionResult> Upload()
        {
            var userId = User.Identity.Name;

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 0)
                return BadRequest("No file uploaded.");

            var postedFile = httpRequest.Files[0];
            var fileName = Path.GetFileName(postedFile.FileName);

            var existing = _db.Documents
                .Where(d => d.UserId == userId && d.FileName == fileName)
                .ToList();

            int newRevision = existing.Count;

            using (var binaryReader = new BinaryReader(postedFile.InputStream))
            {
                var fileData = binaryReader.ReadBytes(postedFile.ContentLength);

                var doc = new Document
                {
                    FileName = fileName,
                    UserId = userId,
                    Data = fileData,
                    Revision = newRevision,
                    UploadedAt = DateTime.UtcNow
                };

                _db.Documents.Add(doc);
                await _db.SaveChangesAsync();
            }

            return Ok(new { message = "Uploaded", revision = newRevision });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{filename}")]
        public IHttpActionResult Download(string filename, int? revision = null)
        {
            var userId = User.Identity.Name;
            var query = _db.Documents.Where(d => d.UserId == userId && d.FileName == filename);

            Document doc = revision.HasValue ?
                query.FirstOrDefault(d => d.Revision == revision.Value) :
                query.OrderByDescending(d => d.Revision).FirstOrDefault();

            if (doc == null)
                return NotFound();

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(doc.Data)
            };

            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = doc.FileName
            };
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return ResponseMessage(result);
        }

    }
}