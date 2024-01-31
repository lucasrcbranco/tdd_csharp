using Domain.Common.Enums;

namespace Domain.Tests.Courses;

public class CourseDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Workload { get; set; }
    public TargetAudience TargetAudience { get; set; }
    public decimal Price { get; set; }
}