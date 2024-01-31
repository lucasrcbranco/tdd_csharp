namespace Domain.Tests.Enrollements;

public class EnrollementDto
{
    public Guid? Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public decimal Price { get; set; }
    public bool Completed { get; set; }
    public decimal Grade { get; set; }
}