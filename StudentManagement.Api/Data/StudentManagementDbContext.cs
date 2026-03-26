using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Domain;

namespace StudentManagement.Api.Data;

public class StudentManagementDbContext : DbContext
{
    public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.StudentId).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Phone).IsUnique();
        });
    }
}
