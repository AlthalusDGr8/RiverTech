# Movies Microservice Technical Demo

**Version: 1.0**

## Structure:

### TechDemo.MoviesDb.Core:

Contains all the basic methods and classes to be used across the application

### TechDemo.MoviesDb.Caching.Memory

Basic Memory Cache implementation

### TechDemo.MoviesDb.Movies

Defines the application entities plus access layer called Managers

### TechDemo.MoviesDb.EntityFrameworkCore

Contains the elements that the system needs to use Entity Framework such as the DbContext plus Repoistory patter implementation

### TechDemo.MoviesDb.GraphQL

Contains the elements to run GraphQL built on top of Entity Framework

### TechDemo.MoviesDb.Orleans

Contains all that is needed to expose a specific Orleans grains

### TechDemo.MoviesDb.API

API ingress application

## Getting Started

1. Download the source code and open using Visual Studio (TechDemo.Movies.sln)
2. Open SQL Management Studio and connect to database using connection string: (LocalDB)\MSSQLLocalDB. This will connect to a local instance of SQL server
3. Under the API project you should find a folder called "Schema\_Setup". Open file "00 - Schema Setup.sql"
4. Modify the paths found under "FILENAME" that make sense to your setup
5. Execute the file. This should create the basic schema plus also insert the sample data
6. If you plan to run Orleans with DB Storage, then execute also "01 - Orleans Query Creation.sql" and "02- Orleans Query Store Storage Creation.sql" (by default we are using Memory and not SQL Server)
7. Set TechDemo.MoviesDb.API as startup project and launch
8. The landing page should be a swagger interface allowing you to invoke the apis from it (swagger/index.html)
9. To test out graphql, please navigate to "ui/playground"

## Things of Note:

- All logs are being fed to console, including Entity Framework SQLs
- All API error responses have been normalised into a uniform error handler for easier website error handling
- All exceptions have a unique identifier to help troubleshoot issues further

