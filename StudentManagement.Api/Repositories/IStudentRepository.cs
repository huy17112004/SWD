using StudentManagement.Api.Domain;

namespace StudentManagement.Api.Repositories;

public interface IStudentRepository
{
    Task<bool> EmailOrPhoneExistsAsync(string email, string phone, CancellationToken cancellationToken = default);
    Task<bool> StudentIdExistsAsync(string studentId, CancellationToken cancellationToken = default);
    Task AddStudentAsync(Student student, CancellationToken cancellationToken = default);
}
