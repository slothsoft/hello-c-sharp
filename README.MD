# Hello C#

[![Build Status](https://app.travis-ci.com/slothsoft/hello-c-sharp.svg?token=pRvfs6NqugbPyMWZEJBY&branch=main)](https://app.travis-ci.com/slothsoft/hello-c-sharp)

This is my first C# web application. It's a broad test to see different features of C# in action.

This application features:

- .Net 6.0 
- ASP Web frontend (HelloCSharp)
- EntityFramework persistence layer (HelloCSharp.Persistence) featuring a SQLite in-memory database
- REST API (HelloCSharp.Rest)
- Tests for all of those

## Helpful Links

- Web application URL after starting: http://localhost:5000/
- REST endpoint URL after starting: http://localhost:5206/swagger
- [SQL Lite Online Database](https://sqliteonline.com/)

## TODO

- [x] rename `Database` into something more unique
- [ ] use lookups instead of these database-based values ?
- [x] run tests in Travis
- [x] create a three-layer tier
- [ ] create productive database
- [x] are unit test projects the way to go now?
- [x] right now definition of database connection and migration is copied in both main application and rest application
