using DapperDb.Data;
using DapperDb.Models;
using DapperDb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddTransient<ICourseService, CourseService>();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Course API

app.MapGet("/Course/Get", async (ICourseService service) =>
{
    var result = await service.GetAllCourses();
    return result.Any() ? Results.Ok(result) : Results.NotFound("No records found");
})
.WithName("GetAllCourses");

app.MapGet("/Course/GetById", async (ICourseService service, string id) =>
{
    var result = await service.GetCourseById(id);
    return result is not null ? Results.Ok(result) : Results.NotFound($"No record found - id: {id}");
})
.WithName("GetCourseById");

app.MapPost("/Course/Create", async (ICourseService service, Course course) =>
{
    bool result = await service.CreateCourse(course);
    return result ? Results.Ok() : Results.BadRequest("Error creating new Course Entity");
})
.WithName("CreateCourse");

app.MapPut("/Course/Update", async (ICourseService service, Course course) =>
{
    bool result = await service.UpdateCourse(course);
    return result ? Results.Ok() : Results.NotFound($"No record found - id: {course.Id}");
})
.WithName("UpdateCourse");

app.MapDelete("/Course/Delete", async (ICourseService service, string id) =>
{
    bool result = await service.DeleteCourse(id);
    return result ? Results.Ok() : Results.NotFound($"No record found - id: {id}");
})
.WithName("DeleteCourse");

#endregion

app.Run();
