namespace DapperDb.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsRecommended { get; set; }
    public bool IsCourseOfTheMonth { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
