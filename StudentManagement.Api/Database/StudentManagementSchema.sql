-- StudentManagementDatabase schema aligned with StudentRepository and Student entity
CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId NVARCHAR(50) NOT NULL UNIQUE,
    FullName NVARCHAR(200) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Phone NVARCHAR(20) NOT NULL UNIQUE,
    Email NVARCHAR(320) NOT NULL UNIQUE,
    Course NVARCHAR(100) NOT NULL,
    InitialLevel NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME()
);
