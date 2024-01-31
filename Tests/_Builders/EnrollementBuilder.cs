using Bogus;
using Domain.Models.Courses;
using Domain.Models.Enrollements;
using Domain.Models.Students;

namespace Domain.Tests._Builders;

public class EnrollementBuilder
{
    private Student _student;
    private Course _course;
    private decimal _price;

    public EnrollementBuilder()
    {
        var faker = new Faker();

        _student = StudentBuilder.New().Build();
        _course = CourseBuilder.New().Build();
        _price = faker.Random.Decimal(1, _course.Price);
    }

    public static EnrollementBuilder New() => new();

    public EnrollementBuilder SetCourse(Course course)
    {
        _course = course;
        return this;
    }

    public EnrollementBuilder SetStudent(Student student)
    {
        _student = student;
        return this;
    }

    public EnrollementBuilder SetPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public Enrollement Build() => new(_course, _student, _price);
}
