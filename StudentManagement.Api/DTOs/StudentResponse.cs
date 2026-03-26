namespace StudentManagement.Api.DTOs;

public class StudentResponse
{
    public string StudentId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
