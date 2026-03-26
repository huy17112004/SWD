using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs;

public class CreateStudentRequest
{
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = default!;

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string Course { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string InitialLevel { get; set; } = default!;
}
