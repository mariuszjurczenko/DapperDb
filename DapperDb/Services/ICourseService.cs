using DapperDb.Models;

namespace DapperDb.Services;

public interface ICourseService
{
    Task<List<Course>> GetAllCourses();
    Task<Course> GetCourseById(string id);
    Task<bool> CreateCourse(Course course);
    Task<bool> UpdateCourse(Course course);
    Task<bool> DeleteCourse(string id);
}
