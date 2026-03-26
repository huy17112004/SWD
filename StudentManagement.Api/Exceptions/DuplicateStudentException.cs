namespace StudentManagement.Api.Exceptions;

public class DuplicateStudentException : Exception
{
    public DuplicateStudentException(string message)
        : base(message)
    {
    }
}
