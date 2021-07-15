# Install

## 1. Local dependencies

Install git (https://git-scm.com/downloads)
Install docker (https://docs.docker.com/get-docker/)

## 2. Backend dependencies

The following environment variables need to be filled in the Docker Compose file "docker-compose.yml" in the root directory.
Fill the missing data marked with <...>.

Setup Kisslog (https://kisslog.net), create a Organization and Application and fill both Ids in Docker Compose.
Setup JwtKey, enter a secure Password for encryption of Authentication Tokens.

## 3. Run

Start a console in the root directory and run 'docker-compose build' and 'docker-compose run'. The backend should be running at Port 812

# Notes
## MongoDB Mapping (Domain Modelling)
https://mongodb.github.io/mongo-csharp-driver/2.8/reference/bson/mapping/
## MongoDB Handling Inheritance
https://codingcanvas.com/storing-polymorphic-classes-in-mongodb-using-c/
## Geo Location
https://docs.microsoft.com/de-de/dotnet/api/system.device.location.geocoordinate?view=netframework-4.8
## Exmaple MongoDB with ASP.NET Core and Identity
https://github.com/coolc0ders/SocialAuthXamarinFormsAspNetCore/blob/master/AuthDemoWeb/AuthDemoWeb/Startup.cs
https://github.com/HueByte/CloudLette/tree/master/src/backend
https://github.com/hamed-shirbandi/CorMon/tree/master/CorMon.Web
## MongoDB with Identity
https://github.com/matteofabbri/AspNetCore.Identity.Mongo
