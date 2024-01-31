using Bogus;
using Domain.Common.Enums;
using Domain.Models.Courses;

namespace Domain.Tests._Builders;

public class CourseBuilder
{
    private string _name;
    private string _description;
    private decimal _workload;
    private TargetAudience _targetAudience;
    private decimal _price;

    public CourseBuilder()
    {
        var faker = new Faker();

        _name = faker.Random.Words(3);
        _description = faker.Lorem.Paragraph();
        _workload = faker.Random.Decimal(6, 40);
        _price = faker.Random.Decimal(5, 1000);

        _targetAudience = TargetAudience.Student;
    }

    public static CourseBuilder New() => new();

    public CourseBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public CourseBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public CourseBuilder SetWorkload(decimal workload)
    {
        _workload = workload;
        return this;
    }

    public CourseBuilder SetTargetAudience(TargetAudience targetAudience)
    {
        _targetAudience = targetAudience;
        return this;
    }

    public CourseBuilder SetPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public Course Build() => new(_name, _description, _workload, _targetAudience, _price);
}
