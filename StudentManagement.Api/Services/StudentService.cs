using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Domain;
using StudentManagement.Api.Exceptions;
using StudentManagement.Api.Repositories;

namespace StudentManagement.Api.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sequence 3.1.1: Service received request to create student for {Email}", request.Email);

        ValidateInputData(request);

        _logger.LogInformation("Sequence 3.1.1.1: Validating duplicate data for {Email}", request.Email);
        await ValidateDuplicateData(request.Email, request.Phone, cancellationToken);

        _logger.LogInformation("Sequence 3.1.1.4: Generating unique Student ID");
        var studentId = await GenerateUniqueStudentIdAsync(cancellationToken);

        var student = new Student
        {
            StudentId = studentId,
            FullName = request.FullName.Trim(),
            DateOfBirth = request.DateOfBirth!.Value,
            Phone = request.Phone.Trim(),
            Email = request.Email.Trim(),
            Course = request.Course.Trim(),
            InitialLevel = request.InitialLevel.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Sequence 3.1.1.5: Saving student {StudentId}", studentId);
        await SaveStudentInfo(student, cancellationToken);

        _logger.LogInformation("Sequence 3.1.1.6: Student profile created for {StudentId}", studentId);

        return new StudentResponse
        {
            StudentId = studentId,
            Message = "Student profile created successfully",
            CreatedAt = student.CreatedAt
        };
    }

    private static void ValidateInputData(CreateStudentRequest request)
    {
        var validationErrors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.FullName))
            validationErrors.Add("Name is required.");
        if (request.DateOfBirth == null)
            validationErrors.Add("Date of birth is required.");
        else if (request.DateOfBirth.Value.Date >= DateTime.UtcNow.Date)
            validationErrors.Add("Date of birth must be in the past.");
        if (string.IsNullOrWhiteSpace(request.Phone))
            validationErrors.Add("Phone is required.");
        if (string.IsNullOrWhiteSpace(request.Email))
            validationErrors.Add("Email is required.");
        if (string.IsNullOrWhiteSpace(request.Course))
            validationErrors.Add("Course is required.");
        if (string.IsNullOrWhiteSpace(request.InitialLevel))
            validationErrors.Add("Initial level is required.");

        if (validationErrors.Any())
            throw new StudentValidationException(string.Join(' ', validationErrors));
    }

    private async Task ValidateDuplicateData(string email, string phone, CancellationToken cancellationToken)
    {
        var exists = await _repository.EmailOrPhoneExistsAsync(email.Trim(), phone.Trim(), cancellationToken);
        if (exists)
        {
            throw new DuplicateStudentException("Student already exists with the provided email or phone.");
        }
    }

    private async Task<string> GenerateUniqueStudentIdAsync(CancellationToken cancellationToken)
    {
        for (var attempt = 0; attempt < 5; attempt++)
        {
            var candidate = $"STU{DateTime.UtcNow:yyyyMMddHHmmssfff}{Random.Shared.Next(1000, 9999)}";
            if (!await _repository.StudentIdExistsAsync(candidate, cancellationToken))
            {
                return candidate;
            }
        }

        throw new InvalidOperationException("Unable to generate a unique Student ID after multiple attempts.");
    }

    private Task SaveStudentInfo(Student student, CancellationToken cancellationToken)
    {
        return _repository.AddStudentAsync(student, cancellationToken);
    }
}
