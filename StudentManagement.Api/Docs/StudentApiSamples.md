# Student Management API Samples

## Create Student Profile

### Request
```http
POST /api/student
Content-Type: application/json

{
  "fullName": "Mai Thi Nguyen",
  "dateOfBirth": "2003-05-24",
  "phone": "+1-202-555-0188",
  "email": "mai.nguyen@example.edu",
  "course": "Computer Science",
  "initialLevel": "Year 1"
}
```

### Success Response (201 Created)
```json
{
  "studentId": "STU20260326123456001",
  "message": "Student profile created successfully",
  "createdAt": "2026-03-26T07:45:12.345Z"
}
```

### Validation Failure (400 Bad Request)
```json
{
  "message": "Name is required. Date of birth is required."
}
```

### Duplicate Data (409 Conflict)
```json
{
  "message": "Student already exists with the provided email or phone."
}
```

Refer to `Database/StudentManagementSchema.sql` for the SQL Server table definition that aligns with `StudentRepository`.
