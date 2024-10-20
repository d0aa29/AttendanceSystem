# Attendance Management System

## Project Description
This Attendance Management System allows employees to check in and check out, submit leave requests, and edit their profiles. The system includes admin and manager roles for managing departments, shifts, and employee requests. It is designed to handle attendance tracking efficiently while offering a streamlined user experience for both employees and administrators.

### Key Features
- **Employee Check-In and Check-Out System**: Employees can record their attendance by checking in when they start their shift and checking out when they finish. This feature tracks the working hours for each employee and stores their attendance data in the system.

- **Leave Requests and Approvals**: Employees can submit requests for leave (vacation, sick days, etc.). Managers or Admins can review and either approve or deny these requests, with statuses updated accordingly. This feature also allows employees to view their submitted requests.

- **Role-Based Access Control (Admin, Manager, Employee)**: The system implements different access levels based on user roles:
  - **Admins** have full control over the system, including managing employees, departments, shifts, leave requests, and user profiles.
  - **Managers** can manage employees within their own department, approve or deny leave requests, and view department-specific information.
  - **Employees** can manage their own profile, check their attendance, submit leave requests, and view their assigned shifts.

- **Department and Shift Management**: Admins and Managers can create and manage departments within the organization, assigning employees to the relevant department. Shifts are also managed in the system, where employees can be assigned to one or multiple shifts. Shifts include details such as start and end times, and employees can edit their assigned shifts with the proper permissions.

- **Employee Profile and Shift Editing**: Employees can update their personal profile information, such as name, contact details, etc. Managers and Admins can also edit employee profiles. Additionally, employees can view and update their shift assignments (with limitations based on their roles). Admins have the authority to reassign employees to different shifts.

Each feature works together to streamline workforce management, from daily attendance to handling administrative tasks like leave and department management.

## Technologies Used
- **ASP.NET Core**: Backend framework.
- **AutoMapper**: For mapping between models and DTOs.
- **Identity**: User authentication and role management.
- **Unit of Work & Repository Pattern**: For managing data access.
- **DTOs**: Used for data transfer across API boundaries.

## NuGet Packages
- **AutoMapper** (12.0.1): Simplifies object-to-object mapping.
- **AutoMapper.Extensions.Microsoft.DependencyInjection** (12.0.1): Integration with Microsoft Dependency Injection.
- **Microsoft.AspNetCore.Authentication.JwtBearer** (8.0.8): Middleware for JWT authentication.
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** (8.0.8): ASP.NET Identity functionality with EF Core.
- **Microsoft.EntityFrameworkCore** (8.0.8): Core library for Entity Framework Core.
- **Microsoft.EntityFrameworkCore.SqlServer** (8.0.8): SQL Server provider for EF Core.
- **Microsoft.EntityFrameworkCore.Tools** (8.0.8): Tools for EF Core migrations and updates.

## Quick Setup Instructions
### Prerequisites
- Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed.
- Install the Entity Framework Core tools if not already installed:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

1. **Clone the repository:**
    ```bash
    git clone https://github.com/d0aa29/AttendanceSystem
    ```

2. **Navigate to the project directory:**
    ```bash
    cd AttendanceManagementSystem
    ```

3. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

4. **Update the database and seed initial data:**
    ```bash
    dotnet ef database update
    ```

5. **Run the project:**
    ```bash
    dotnet run
    ```

## API Endpoints
Refer to the `API.md` for more details about API endpoints.

## Database Design
Refer to the `Database.md` for an overview of the database schema.

## Documentation Folder Structure
- `README.md`: Project overview and setup instructions.
- `SystemArchitecture.md`: Overview of system architecture, components, and design patterns.
- `API.md`: List of API endpoints and descriptions.
- `Database.md`: Database Tables and relationships.
