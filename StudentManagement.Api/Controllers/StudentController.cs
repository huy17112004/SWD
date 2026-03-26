using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Exceptions;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentService studentService, ILogger<StudentController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Sequence 3: Controller passed request to service for {Email}", request.Email);
            var response = await _studentService.CreateStudentAsync(request, cancellationToken);
            return CreatedAtAction(nameof(Create), new { studentId = response.StudentId }, response);
        }
        catch (StudentValidationException validationException)
        {
            _logger.LogWarning(validationException, "Sequence 3.1.1.3: Validation failed for student creation");
            return BadRequest(new { validationException.Message });
        }
        catch (DuplicateStudentException duplicateException)
        {
            _logger.LogWarning(duplicateException, "Sequence E.5.1: Duplicate student data detected");
            return Conflict(new { duplicateException.Message });
        }
    }
}
