using Dapper;
using DapperDb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDb.Data;

public class CourseRepository
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public CourseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<List<Course>> GetAll()
    {
        try
        {
            _dbConnection?.Open();

            string query = @"select id, name, author, price, imageUrl, isRecommended, isCourseOfTheMonth, startDate, endDate from course";

            var courses = await _dbConnection.QueryAsync<Course>(query);
            return courses.ToList();
        }
        catch (Exception)
        {
            return new List<Course>();
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<Course?> GetById(string id)
    {
        try
        {
            _dbConnection?.Open();

            string query = $@"select id, name, author, price, imageUrl, isRecommended, isCourseOfTheMonth, startDate, endDate from course where id = '{id}'";

            var course = await _dbConnection.QueryAsync<Course>(query, id);
            return course.FirstOrDefault();
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Create(Course course)
    {
        try
        {
            _dbConnection?.Open();

            string query = @"insert into course(name, author, price, imageUrl, isRecommended, isCourseOfTheMonth, startDate, endDate) 
                             values(@Name, @Author,  @Price, @ImageUrl, @IsRecommended, @IsCourseOfTheMonth, @StartDate, @EndDate)";

            await _dbConnection.ExecuteAsync(query, course);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Update(Course course)
    {
        try
        {
            _dbConnection?.Open();

            string selectQuery = $@"select * from course where id = '{course.Id}'";

            var entity = await _dbConnection.QueryAsync<Course>(selectQuery, course.Id);

            if (entity is null)
                return false;

            string updateQuery = @"update course set name = @Name, author = @Author, price = @Price, imageUrl = @ImageUrl, 
                                   isRecommended = @IsRecommended, isCourseOfTheMonth = @IsCourseOfTheMonth, startDate = @StartDate, endDate = @EndDate 
                                   where id = @Id";

            await _dbConnection.ExecuteAsync(updateQuery, course);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Delete(string id)
    {
        try
        {
            _dbConnection?.Open();

            string selectQuery = $@"select * from course where id = '{id}'";

            var entity = await _dbConnection.QueryAsync<Course>(selectQuery, id);

            if (entity is null)
                return false;

            string query = $@"delete from course where id = '{id}'";

            await _dbConnection.ExecuteAsync(query);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }
}
