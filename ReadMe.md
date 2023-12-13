# Global Web API

## Overview

The Global Web API is a RESTful API built with .NET 6 and Entity Framework Core to manage users and products. 

It exposes endpoints for CRUD operations on users and products. The API follows a layered architecture with controller, services, data access and domain model layers.

## Contents

- [Technologies](#technologies)
- [Architecture](#architecture)
- [Models](#models)
- [Controllers](#controllers)
- [Services](#services)  
- [Data Access](#data-access)
- [Dependencies](#dependencies) 
- [Testing](#testing)
- [How to Run](#how-to-run)
- [API Documentation](#api-documentation)
- [Auth](#auth) 
- [Deployment](#deployment)

## Technologies

- .NET 6
- Entity Framework Core 
- SQLite
- AutoMapper
- Swagger UI

## Architecture

The solution contains the following projects:

**global_web_api** - The core project containing controllers, services, data access and models.

**web_api_test** - Unit test project using xUnit, Moq and InMemory database.

It follows layered architecture with separation of concerns:

- **Controllers** - Handle HTTP requests and responses. Uses services for business logic.
- **Services** - All business logic for operations on entities.
- **Data Access** - Database access using Entity Framework Core.
- **Models** - Domain models for the API representing entities.
- **DTOs** - Data transfer objects used to pass data between layers.
- **Interfaces** - Abstractions defining service contracts.
- **Mappings** - AutoMapper mappings between models and DTOs.

## Models

- **User** - Represents an API user 
- **Product** - Represents a product sold by the API

## Controllers

- **UsersController** - Handles user CRUD operations
- **ProductsController** - Handles product CRUD operations

## Services

- **UserService** - Handles business logic for users
- **ProductService** - Handles business logic for products

## Data Access 

Uses Entity Framework Core with SQLite database. Database context and configurations are specified in **Context** folder.

Migrations can be used to manage database schema changes.

## Dependencies

See Packages for .NET SDK version.

Key dependencies include:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Newtonsoft.Json
- LinqKit
- FluentAssertions
- Moq

## Testing

Unit and integration tests cover:

- Controllers
- Services

xUnit, Moq and InMemory databases are used for unit testing.

## How to run

```

```

The API will run on https://localhost:7255 by default.

## API Documentation

Swagger UI showing API endpoints is available at /swagger.

## Auth

(Comin soon) Auth using JWT tokens will be added in upcoming sprints.

## Deployment 

The API can be containerized in Docker and deployed to Kubernetes cluster.
