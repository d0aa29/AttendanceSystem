# API Endpoints Documentation

## Authentication Endpoints
- **Register**:  
  `POST /api/Authentication/Register`  
  Allows a new user to register.

- **Login**:  
  `POST /api/Authentication/Login`  
  Authenticates the user and provides a token.

---

## Attendance Record Endpoints
- **Get All Attendance Records**:  
  `GET /api/AttendanceRecord/AllAttendanceRecord`  
  Returns a list of all employee attendance records.

- **Get My Attendance Record**:  
  `GET /api/AttendanceRecord/MyAttendanceRecord`  
  Retrieves the attendance record of the logged-in employee.

- **Check In**:  
  `POST /api/AttendanceRecord/CheckIn`  
  Employee checks in at the start of their shift.

- **Check Out**:  
  `PUT /api/AttendanceRecord/CheckOut`  
  Employee checks out at the end of their shift.

---

## Department Endpoints
- **Get Departments**:  
  `GET /api/Department`  
  Retrieves a list of all departments.

- **Get Department by ID**:  
  `GET /api/Department/{id}`  
  Retrieves a specific department by its ID.

- **Create Department**:  
  `POST /api/Department`  
  Allows an admin to create a new department.

- **Update Department**:  
  `PUT /api/Department/{id}`  
  Updates a department's information.

- **Delete Department**:  
  `DELETE /api/Department/{id}`  
  Deletes a department by its ID.

---

## Employee Endpoints
- **Get My Profile**:  
  `GET /api/Employee/Myprofile`  
  Retrieves the profile of the logged-in employee.

- **Edit My Profile**:  
  `PUT /api/Employee/EditMyprofile`  
  Allows the logged-in employee to update their profile.

- **Edit My Shifts**:  
  `PUT /api/Employee/EditMyShifts`  
  Allows the logged-in employee to update their assigned shifts.

- **Get All Employees**:  
  `GET /api/Employee`  
  Retrieves a list of all employees.

- **Get Employee by ID**:  
  `GET /api/Employee/{id}`  
  Retrieves a specific employee by their ID.

- **Create Employee**:  
  `POST /api/Employee`  
  Admin can create a new employee.

- **Update Employee**:  
  `PUT /api/Employee/{id}`  
  Updates an employee's information.

- **Delete Employee**:  
  `DELETE /api/Employee/{id}`  
  Deletes an employee by their ID.

---

## Leave Request Endpoints
- **Get My Leave Requests**:  
  `GET /api/Request/MyRequests`  
  Retrieves all leave requests made by the logged-in employee.

- **Submit Leave Request**:  
  `POST /api/Request`  
  Submits a new leave request.

- **Get All Leave Requests**:  
  `GET /api/Request`  
  Admin or manager can retrieve a list of all leave requests.

- **Update My Leave Request**:  
  `PUT /api/Request/UpdateMyRequests`  
  Allows the logged-in employee to update their own leave request.

- **Update Leave Request by ID**:  
  `PUT /api/Request/update/{id}`  
  Admin or manager can update a specific leave request by its ID.

- **Delete Leave Request by ID**:  
  `DELETE /api/Request/{id}`  
  Deletes a specific leave request by its ID.

- **Get Leave Request by ID**:  
  `GET /api/Request/{id}`  
  Retrieves a specific leave request by its ID.

- **Approve Leave Request**:  
  `PUT /api/Request/approve/{id}`  
  Admin or manager can approve a leave request.

- **Deny Leave Request**:  
  `PUT /api/Request/deny/{id}`  
  Admin or manager can deny a leave request.

---

## Shift Endpoints
- **Get All Shifts**:  
  `GET /api/Shift`  
  Retrieves a list of all shifts.

- **Get Shift by ID**:  
  `GET /api/Shift/{id}`  
  Retrieves a specific shift by its ID.

- **Create Shift**:  
  `POST /api/Shift`  
  Admin can create a new shift.

- **Update Shift by ID**:  
  `PUT /api/Shift/{id}`  
  Updates a specific shift by its ID.

- **Delete Shift by ID**:  
  `DELETE /api/Shift/{id}`  
  Deletes a specific shift by its ID.

---

## Users Endpoints
- **Get All Users**:  
  `GET /api/Users`  
  Retrieves a list of all registered users.

- **Get User by ID**:  
  `GET /api/Users/{id}`  
  Retrieves a specific user by their ID.

- **Update User**:  
  `PUT /api/Users/{id}`  
  Updates a specific user's information.

- **Delete User**:  
  `DELETE /api/Users/{id}`  
  Deletes a specific user by their ID.
