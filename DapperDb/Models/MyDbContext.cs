using Microsoft.EntityFrameworkCore;

namespace DapperDb.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
}
