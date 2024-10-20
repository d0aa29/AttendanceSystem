## Tables

### AspNetRoles
- **Id** (nvarchar(450), PK)
- **Name** (nvarchar(256))
- **NormalizedName** (nvarchar(256))
- **ConcurrencyStamp** (nvarchar(max))

### AspNetRoleClaims
- **Id** (int, PK)
- **RoleId** (nvarchar(450), FK to AspNetRoles(Id))
- **ClaimType** (nvarchar(max))
- **ClaimValue** (nvarchar(max))

### AspNetUsers
- **Id** (nvarchar(450), PK)
- **UserName** (nvarchar(256))
- **NormalizedUserName** (nvarchar(256))
- **Email** (nvarchar(256))
- **NormalizedEmail** (nvarchar(256))
- **EmailConfirmed** (bit)
- **PasswordHash** (nvarchar(max))
- **SecurityStamp** (nvarchar(max))
- **ConcurrencyStamp** (nvarchar(max))
- **PhoneNumber** (nvarchar(max))
- **PhoneNumberConfirmed** (bit)
- **TwoFactorEnabled** (bit)
- **LockoutEnd** (datetimeoffset(7))
- **LockoutEnabled** (bit)
- **AccessFailedCount** (int)

### AspNetUserClaims
- **Id** (int, PK)
- **UserId** (nvarchar(450), FK to AspNetUsers(Id))
- **ClaimType** (nvarchar(max))
- **ClaimValue** (nvarchar(max))

### AspNetUserLogins
- **LoginProvider** (nvarchar(450), PK)
- **ProviderKey** (nvarchar(450), PK)
- **ProviderDisplayName** (nvarchar(max))
- **UserId** (nvarchar(450), FK to AspNetUsers(Id))

### AspNetUserRoles
- **UserId** (nvarchar(450), PK, FK to AspNetUsers(Id))
- **RoleId** (nvarchar(450), PK, FK to AspNetRoles(Id))

### AspNetUserTokens
- **UserId** (nvarchar(450), PK)
- **LoginProvider** (nvarchar(450), PK)
- **Name** (nvarchar(450), PK)
- **Value** (nvarchar(max))

### AttendanceRecords
- **Id** (int, PK)
- **Date** (datetime2(7))
- **CheckIn** (time(7), NOT NULL)
- **CheckOut** (time(7))
- **OutStatus** (nvarchar(max))
- **InStatus** (nvarchar(max), NOT NULL)
- **Note** (nvarchar(max))
- **EmployeeId** (int, FK to Employees(Id))
- **ShiftId** (int, FK to Shifts(Id))

### Departments
- **Id** (int, PK)
- **Name** (nvarchar(max), NOT NULL)
- **Location** (nvarchar(max))

### Employees
- **Id** (int, PK)
- **SSN** (int, NOT NULL)
- **Name** (nvarchar(100), NOT NULL)
- **ImgUrl** (nvarchar(max))
- **Gender** (nvarchar(max))
- **BirthDate** (datetime2(7))
- **JoinedOn** (datetime2(7))
- **DepartmentId** (int, FK to Departments(Id))
- **UserId** (nvarchar(450), FK to AspNetUsers(Id))

### EmployeeShift
- **EmployeeId** (int, PK, FK to Employees(Id))
- **ShiftId** (int, PK, FK to Shifts(Id))

### LeaveRequests
- **Id** (int, PK)
- **StartDate** (time(7))
- **EndDate** (time(7))
- **Reason** (nvarchar(max))
- **ApprovalStatus** (nvarchar(max), NOT NULL)
- **EmployeeId** (int, FK to Employees(Id))
- **RequestDate** (datetime2(7), NOT NULL)

### Shifts
- **Id** (int, PK)
- **Num** (int, NOT NULL)
- **From** (time(7), NOT NULL)
- **To** (time(7), NOT NULL)

## Relationships
- **AspNetUsers** to **Employees**: One-to-One (1 User can be linked to 1 Employee)
- **Departments** to **Employees**: One-to-Many (1 Department can have many Employees)
- **Employees** to **AttendanceRecords**: One-to-Many (1 Employee can have many AttendanceRecords)
- **Employees** to **LeaveRequests**: One-to-Many (1 Employee can have many LeaveRequests)
- **Shifts** to **AttendanceRecords**: One-to-Many (1 Shift can be assigned to many AttendanceRecords)
- **Employees** to **Shifts**: Many-to-Many (Many Employees can be assigned to Many Shifts through the **EmployeeShift** table)

---
