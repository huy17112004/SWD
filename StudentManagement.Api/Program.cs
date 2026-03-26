using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Repositories;
using StudentManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Sequence alignment: StaffApp ? StudentController ? StudentService ? StudentRepository ? Database System

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StudentManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentManagementDatabase")
        ?? throw new InvalidOperationException("Connection string 'StudentManagementDatabase' is not configured.")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.DocumentTitle = "Student Management API");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
