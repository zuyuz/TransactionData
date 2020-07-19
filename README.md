# TransactionData sample application
<br/>

This solution demonstrates how to create API, that accepts XML and CSV data files, containing simple "transaction" model, records and query to the data store, while logging all possible exceptions on the way.


## Technologies
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [ASP.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Entity Framework Core 3.1](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.6?_src=template)
* [MediatR](https://www.nuget.org/packages/MediatR/8.0.2?_src=template)
* [AutoMapper](https://www.nuget.org/packages/AutoMapper/10.0.0?_src=template)
* [CSharpFunctionalExtensions](https://www.nuget.org/packages/CSharpFunctionalExtensions/2.10.0?_src=template)
* [CsvHelper](https://www.nuget.org/packages/CsvHelper/15.0.5?_src=template)
* [Serilog](https://www.nuget.org/packages/Serilog.Extensions.Logging.File/2.0.0?_src=template)
* [Moq](https://www.nuget.org/packages/Moq/4.5.28?_src=template)
* [xUnit](https://www.nuget.org/packages/xunit/2.4.0?_src=template)

## Overview

### Application Core

####  [TransactionData.Domain](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Domain)

This project contains domain of application, which includes: commands, constants, dtos, enums, events, models and queries.

#### [TransactionData.Service.Interfaces](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Service.Interfaces)

This project contains abstractions for TransactionData.Service.

### Application Infrastructure

#### [TransactionData.Data](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Data)

This project contains all entityConfigurations, repositories and DbContext.

#### [TransactionData.Data.Entities](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Data.Entities)

This project contains all entities, that are used to generate tables, using EF Core code-first approach.

#### [TransactionData.Data.Interfaces](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Data.Interfaces)

This project contains data contractor interfaces, abstractions, that are used with Dependency injection to provide loose coupling between application and data provider.

#### [TransactionData.Data.MSSql](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Data.MSSql)

This project contains Migrations for MSSQL database.

#### [TransactionData.Data.Sqlite](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Data.Sqlite)

This project contains Migrations for Sqlite database.

#### [TransactionData.Service](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.Service)

This project contains command, query and event handlers for managing application requests.

#### [TransactionData.IoC](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.IoC)

This project represents Inversion of control, powered by ASP.NET Core Dependency injection mechanism. Introduces loose coupling between projects.

### Outer Layer

#### [TransactionData.WebUI](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.WebUI)

This project is based on ASP.NET Core 3.1. Represents outer layer of application, WebAPI. Contains user friendly interface, powered by Swagger.

### Tests

#### [TransactionData.UnitTests](https://github.com/zuyuz/TransactionData/tree/master/TransactionData.UnitTests)

This project contains application unit tests.

