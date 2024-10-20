# System Architecture

## Key Components:
- **Authentication**: Managed by ASP.NET Identity.
- **Database**: SQL Server using Entity Framework Core.
- **Role-based Access Control**: Divides system access between Admin, Manager, and Employee.
- **Services**: Business logic is managed through service classes and repository pattern.

## Design Patterns:
-Repository Pattern: Used to manage data access logic.
-Unit of Work: Ensures a single transaction scope for all repository changes.
-Data Transfer Object (DTO) Pattern: Utilized alongside AutoMapper to streamline data exchange between the application layers.

## Diagrams
No Diagrams Ye
