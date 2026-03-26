using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Domain;

namespace StudentManagement.Api.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentManagementDbContext _dbContext;

    public StudentRepository(StudentManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> EmailOrPhoneExistsAsync(string email, string phone, CancellationToken cancellationToken = default)
    {
        return _dbContext.Students
            .AnyAsync(x => x.Email == email || x.Phone == phone, cancellationToken);
    }

    public Task<bool> StudentIdExistsAsync(string studentId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Students
            .AnyAsync(x => x.StudentId == studentId, cancellationToken);
    }

    public async Task AddStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _dbContext.Students.AddAsync(student, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
