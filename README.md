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

## Overview

### TransactionData.Data

This project contains all entityConfigurations, repositories and DbContext.

#### TransactionData.Data.Entities

This project contains all entities, that are used to generate tables, using EF Core code-first approach.

#### TransactionData.Data.Interfaces

This project contains data contractor interfaces, that are used with Dependency injection to provide loose coupling between application and data provider.

#### TransactionData.Data.MSSql

This project contains Migrations for MSSQL database.

#### TransactionData.Data.Sqlite

This project contains Migrations for Sqlite database.

### TransactionData.Service

This project contains command, query and event handlers for managing application requests.

###  TransactionData.Domain

This project contains domain of application, which includes: commands, constants, dtos, enums, events, models and queries.

###  TransactionData.IoC

This project represents Inversion of control, powered by ASP.NET Core DI mechanism. Introduces loose coupling between projects.

### TransactionData.WebUI

This layer is based on ASP.NET Core 3.1. Represents outer layer of application. Contains user friendly interface, powered by Swagger.
