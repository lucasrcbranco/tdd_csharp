using Bogus;
using Domain._Utils;
using Domain.Common.Enums;
using Domain.Models.Courses;
using Domain.Tests._Builders;
using Domain.Tests._Utils;
using ExpectedObjects;

namespace Domain.Tests.Courses;

/// <summary>
/// USER STORY:
/// Eu, enquanto administrador, quero criar e editar cursos que sejam abertas matrículas
/// para o mesmo.
/// Critério de Aceite:
///     1. Criar um curso com nome(string), descrição(string), carga horária(decimal), público alvo(enumerador) e valor(decimal).
///     2. As opções para público alvo são: Estudante, Universitário, Empregado e Empreendedor.
///     3. Todos os campos são obrigatórios.
/// </summary>
public class CourseTests
{
    private readonly string _name;
    private readonly decimal _workload;
    private readonly TargetAudience _targetAudience;
    private readonly decimal _price;
    private readonly string _description;

    public CourseTests()
    {
        var faker = new Faker();

        _name = faker.Random.Words(3);
        _workload = faker.Random.Decimal(1, 100);
        _price = faker.Random.Decimal(1, 300);
        _description = faker.Lorem.Paragraph();

        _targetAudience = TargetAudience.Student;
    }

    [Fact(DisplayName = "ShouldCreateCourse")]
    public void ShouldCreateCourse()
    {
        // Arrange
        var expectedCourse = new
        {
            Name = _name,
            Description = _description,
            Workload = _workload,
            Price = _price,
            TargetAudience = _targetAudience
        };

        // Action
        var course = new Course(
            expectedCourse.Name,
            expectedCourse.Description,
            expectedCourse.Workload,
            expectedCourse.TargetAudience,
            expectedCourse.Price);

        // Assert
        expectedCourse.ToExpectedObject().ShouldMatch(course);
    }

    [Theory(DisplayName = "ShouldNotCreateCourseWithInvalidName")]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldNotCreateCourseWithInvalidName(string invalidName)
    {
        Assert
            .Throws<ArgumentException>(() => CourseBuilder.New().SetName(invalidName).Build())
            .AssertExceptionWithMessageCheck(Globals.CourseInvalidName);
    }

    [Theory(DisplayName = "ShouldNotCreateCourseWithInvalidDescription")]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldNotCreateCourseWithInvalidDescription(string invalidDescription)
    {
        Assert
            .Throws<ArgumentException>(() => CourseBuilder.New().SetDescription(invalidDescription).Build())
            .AssertExceptionWithMessageCheck(Globals.CourseInvalidDescription);
    }

    [Theory(DisplayName = "ShouldNotCreateCourseWithInvalidWorkload")]
    [InlineData(0)]
    [InlineData(-10)]
    [InlineData(-255)]
    [InlineData(-1000)]
    public void ShouldNotCreateCourseWithInvalidWorkload(decimal invalidWorkload)
    {
        Assert
            .Throws<ArgumentException>(() => CourseBuilder.New().SetWorkload(invalidWorkload).Build())
            .AssertExceptionWithMessageCheck(Globals.CourseInvalidWorkload);

    }

    [Theory(DisplayName = "ShouldNotCreateCourseWithInvalidPrice")]
    [InlineData(0)]
    [InlineData(-10)]
    [InlineData(-255)]
    [InlineData(-1000)]
    public void ShouldNotCreateCourseWithInvalidPrice(decimal invalidPrice)
    {
        Assert
            .Throws<ArgumentException>(() => CourseBuilder.New().SetPrice(invalidPrice).Build())
            .AssertExceptionWithMessageCheck(Globals.CourseInvalidPrice);
    }
}
