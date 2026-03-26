using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Domain;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string StudentId { get; set; } = default!;

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = default!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(20)]
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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
