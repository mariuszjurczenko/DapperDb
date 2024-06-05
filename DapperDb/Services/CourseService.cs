using DapperDb.Data;
using DapperDb.Models;

namespace DapperDb.Services;

public class CourseService : ICourseService
{
    private readonly CourseRepository _repository;

    public CourseService(CourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Course>> GetAllCourses() =>
        await _repository.GetAll();

    public async Task<Course> GetCourseById(string id) =>
        await _repository.GetById(id);

    public async Task<bool> CreateCourse(Course course) =>
        await _repository.Create(course);

    public async Task<bool> UpdateCourse(Course course) =>
        await _repository.Update(course);

    public async Task<bool> DeleteCourse(string id) =>
        await _repository.Delete(id);
}
