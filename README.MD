# Hello C#

[![Build Status](https://github.com/slothsoft/hello-c-sharp/actions/workflows/build.yml/badge.svg)](https://github.com/slothsoft/hello-c-sharp/actions)

This is my first C# web application. It's a broad test to see different features of C# in action.

**URL:** [hellocsharp.azurewebsites.net](https://hellocsharp.azurewebsites.net/)

This application features:

- .Net 6.0 
- ASP Web frontend (HelloCSharp.Frontend)
- EntityFramework persistence layer (HelloCSharp.Persistence) featuring a SQLite in-memory database
- API for separating frontend from persistence layer (HelloCSharp.Api)
- REST API (HelloCSharp.Rest)
- Tests for all of those
- HelloCSharp is the application starter
- continuous integration using [GitHub Actions](https://github.com/slothsoft/hello-c-sharp/actions)



## TODO

<details>
  <summary>Already done...</summary>

- [x] rename `Database` into something more unique
- [x] use lookups instead of these database-based values?
- [x] run tests in Travis
- [x] create a three-layer tier
- [x] are unit test projects the way to go now?
- [x] right now definition of database connection and migration is copied in both main application and rest application
- [x] `AbstractRepositoryController`: it's not good that the ID is in the URL and the TValue
- [x] tests for endpoint and controller test for create and update are missing
- [x] test for endpoint "relationshiptype" is missing
- [x] is it possible to start REST and webapp in one application?
- [x] would it be better to inject repositories directly?
- [x] I18N
- [x] endpoint "relationshiptypes" should be string-based, not index-based in REST
- [x] deploy to Azure!
</details>
  
- [ ] create productive database
- [ ] endpoint for "relationship" is missing
- [ ] Swashbuckle dependency looks incorrect
- [ ] frontend tests
- [ ] way better error handling for the REST API
- [ ] show language selecter so people know it's localized
- [ ] collapsible navbar does not work (probably because I deleted though JS frameworks)



## Helpful Stuff

### Links

- Application Links (see [launchSettings.json](HelloCSharp/Properties/launchSettings.json))
  - Web application: http://localhost:5000/
  - Swagger: http://localhost:5000/swagger
  - REST endpoint: http://localhost:5000/api
- [SQL Lite Online Database](https://sqliteonline.com/)


### Travis

Since Travis was replaced by GitHub Actions, this was the old file to build .NET 6.0 solutions: [.travis.yml](../../blob/6564ef8e139409da7fd6f51004822a719961d924/.travis.yml). 


### GitHub Actions

GitHub Actions was added with [commit 3f5ffcc](../../commit/3f5ffccc324022379f7a954bdf169c97c1d4fe28). 

The important file for .NET 6.0 projects are:

- [build.yml](.github/workflows/build.yml) - defines the entire build job
- [action.yml](action.yml) - a specialized action to install .NET Core SDK
- [index.js](.github/dist/index.js) - this file is used by the _action.yml_ to install .NET Core


