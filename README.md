# Blog Project

This project encompasses a small-scale blog application developed using ASP.NET Core.

## Project Structure

The project is structured into the following layers:

- **API:** Server-side application programming interface.
- **Application:** Services and service interfaces containing application logic.
- **Domain:** Core business objects and business rules.
- **Infrastructure:** Infrastructure layer, handling database access and storage operations.
- **Presentation:** User interface, pages, and view models.
- **DTO and VM:** Common layer containing Data Transfer Objects (DTO) and View Models (VM).

## Technological Details

- **Database:** Microsoft SQL Server is used.
- **Authentication and Authorization:** User control is managed using Identity on the backend, and restriction is imposed on the frontend using JWT tokens.
- **Dependency Injection:** Dependencies are managed using Autofac.
- **DTO Mapping:** Data Transfer Objects (DTOs) mapping is accomplished using the AutoMapper library.
- **Repository Pattern:** Abstract repositories in the Domain layer, concrete repositories in the Infrastructure layer.


