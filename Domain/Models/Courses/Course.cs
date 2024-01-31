using Domain._Utils;
using Domain.Common.Enums;
using Domain.Common.Guards;
using Domain.Models.Base;

namespace Domain.Models.Courses;

public class Course : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Workload { get; private set; }
    public TargetAudience TargetAudience { get; private set; }
    public decimal Price { get; private set; }

    private Course()
    {

    }

    public Course(
        string name,
        string description,
        decimal workload,
        TargetAudience targetAudience,
        decimal price)
    {
        Guards.NotNullOrEmpty(name, Globals.CourseInvalidName);
        Guards.NotNullOrEmpty(description, Globals.CourseInvalidDescription);
        Guards.GreaterThanZero(workload, Globals.CourseInvalidWorkload);
        Guards.GreaterThanZero(price, Globals.CourseInvalidPrice);

        Name = name;
        Description = description;
        Workload = workload;
        TargetAudience = targetAudience;
        Price = price;
    }

    public void Update(
        string name,
        string description,
        decimal workload,
        TargetAudience targetAudience,
        decimal price)
    {
        Guards.NotNullOrEmpty(name, Globals.CourseInvalidName);
        Guards.NotNullOrEmpty(description, Globals.CourseInvalidDescription);
        Guards.GreaterThanZero(workload, Globals.CourseInvalidWorkload);
        Guards.GreaterThanZero(price, Globals.CourseInvalidPrice);

        Name = name;
        Description = description;
        Workload = workload;
        TargetAudience = targetAudience;
        Price = price;
    }
}
