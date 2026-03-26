using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Services;

public interface IStudentService
{
    Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);
}
