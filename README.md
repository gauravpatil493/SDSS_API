# sdss-api
1. How to build and run the backend

Open your solution in Visual Studio.

Make sure the SDSS_API project is set as the Startup Project.

Press F5 or Ctrl+F5 to run it.
use this url on postman post method
https://localhost:44329/api/auth/login
{
    "Username":"test",
    "Password":"1234"
}
Expected Response
{"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJuYW1laWQiOiJ0ZXN0IiwibmJmIjoxNzQ3NDAxNjA0LCJleHAiOjE3NDc0MDUyMDIsImlhdCI6MTc0NzQwMTYwNCwiaXNzIjoiRG9jdW1lbnRTdG9yYWdlQVBJIiwiYXVkIjoiRG9jdW1lbnRTdG9yYWdlVXNlcnMifQ.oTf0qCJWYlGmV2wrOg79vsYQWsq_4Jmyv2Md0R9GB4o"}


2.Upload a Document

https://localhost:44329/api/documents/upload

Postman Setup - > Method: POST

Authorization: - > Type: Bearer Token 

"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJuYW1laWQiOiJ0ZXN0IiwibmJmIjoxNzQ3NDAxNjA0LCJleHAiOjE3NDc0MDUyMDIsImlhdCI6MTc0NzQwMTYwNCwiaXNzIjoiRG9jdW1lbnRTdG9yYWdlQVBJIiwiYXVkIjoiRG9jdW1lbnRTdG9yYWdlVXNlcnMifQ.oTf0qCJWYlGmV2wrOg79vsYQWsq_4Jmyv2Md0R9GB4o"

Body → form-data:

Key: file (type: File)

Value: Select a file to upload from your computer.
{
  "message": "Uploaded",
  "revision": 0
}

3.Download the Document

Endpoint
http://localhost:12345/api/documents/{filename}

You can also add a revision parameter if needed:

GET http://localhost:12345/api/documents/{filename}?revision=1

how 
