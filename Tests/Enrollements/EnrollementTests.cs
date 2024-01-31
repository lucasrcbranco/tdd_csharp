using Bogus;
using Domain._Utils;
using Domain.Common.Enums;
using Domain.Models.Courses;
using Domain.Models.Enrollements;
using Domain.Models.Students;
using Domain.Tests._Builders;
using Domain.Tests._Utils;
using ExpectedObjects;

namespace Domain.Tests.Enrollements;

/// <summary>
/// USER STORY:
/// Eu, enquanto administrador, quero informar um estudante e um curso para realizar a matrícula deste usuário.
/// Eu, enquanto administrador, quero informar um valor pago por esta matrícula, não podendo ser superior ao do curso, mas podendo ser inferior e gerando um desconto.
/// Eu, enquanto administrador, quero informar a nota de um aluno para concluir o curso.
/// CONSTRAINTS:
/// Matrícula deve possuir curso e estudante válidos(não nulos)
/// Matrícula não deve possuir preço maior que o do curso.
/// Matrícula deve preencher valor desconto e possui desconto com base no valor da matrícula caso seja inferior ao do curso.
/// Matrícula ter uma nota.
/// Matrícula indicar que um curso foi concluído.
/// </summary>
public class EnrollementTests
{
    private readonly Student _student;
    private readonly Course _course;
    private readonly decimal _price;

    public EnrollementTests()
    {
        var faker = new Faker();

        _student = StudentBuilder.New().Build();
        _course = CourseBuilder.New().Build();
        _price = faker.Random.Decimal(1, _course.Price);
    }

    [Fact(DisplayName = "ShouldCreateEnrollement")]
    public void ShouldCreateEnrollement()
    {
        // Arrange
        var expectedEnrollement = new
        {
            Course = _course,
            Student = _student,
            Price = _price
        };

        // Action
        var enrollement = new Enrollement(
            expectedEnrollement.Course,
            expectedEnrollement.Student,
            expectedEnrollement.Price);

        // Assert
        expectedEnrollement.ToExpectedObject().ShouldMatch(enrollement);
    }

    [Theory(DisplayName = "ShouldNotCreateEnrollementWithInvalidCourse")]
    [InlineData(null)]
    public void ShouldNotCreateEnrollementWithInvalidCourse(Course invalidCourse)
    {
        Assert
            .Throws<ArgumentException>(() => EnrollementBuilder.New().SetCourse(invalidCourse).Build())
            .AssertExceptionWithMessageCheck(Globals.EnrollementWithInvalidCourse);
    }

    [Theory(DisplayName = "ShouldNotCreateEnrollementWithInvalidStudent")]
    [InlineData(null)]
    public void ShouldNotCreateEnrollementWithInvalidEmail(Student invalidStudent)
    {
        Assert
            .Throws<ArgumentException>(() => EnrollementBuilder.New().SetStudent(invalidStudent).Build())
            .AssertExceptionWithMessageCheck(Globals.EnrollementWithInvalidStudent);
    }

    [Theory(DisplayName = "ShouldNotCreateEnrollementWithInvalidPrice")]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-99)]
    public void ShouldNotCreateEnrollementWithInvalidPrice(decimal invalidPrice)
    {
        Assert
            .Throws<ArgumentException>(() => EnrollementBuilder.New().SetPrice(invalidPrice).Build())
            .AssertExceptionWithMessageCheck(Globals.EnrollementWithInvalidPrice);

    }

    [Fact(DisplayName = "ShouldNotCreateEnrollementWithPriceGreaterThanCoursePrice")]
    public void ShouldNotCreateEnrollementWithPriceGreaterThanCoursePrice()
    {
        var course = CourseBuilder.New().Build();

        var enrollementPriceGreaterThanCoursePrice = course.Price + 1;

        Assert
            .Throws<ArgumentException>(() => EnrollementBuilder.New().SetCourse(course).SetPrice(enrollementPriceGreaterThanCoursePrice).Build())
            .AssertExceptionWithMessageCheck(Globals.EnrollementWithPriceGreaterThanCoursePrice);
    }

    [Fact(DisplayName = "ShouldCreateEnrollementWithDiscount")]
    public void ShouldCreateEnrollementWithDiscount()
    {
        var course = CourseBuilder.New().Build();
        var enrollementPriceLowerThanCoursePrice = course.Price - 1;
        var enrollement = EnrollementBuilder.New().SetCourse(course).SetPrice(enrollementPriceLowerThanCoursePrice).Build();
        var discountedAmount = enrollement.Course.Price - enrollementPriceLowerThanCoursePrice;

        Assert.True(enrollement.WasDiscounted && enrollement.Discount == discountedAmount);
    }

    [Fact(DisplayName = "ShouldNotCreateEnrollementWithCourseAndStudentHavingDifferentTargetAudiences")]
    public void ShouldNotCreateEnrollementWithCourseAndStudentHavingDifferentTargetAudiences()
    {
        var course = CourseBuilder.New().SetTargetAudience(TargetAudience.Bussiness).Build();
        var student = StudentBuilder.New().SetTargetAudience(TargetAudience.Student).Build();

        Assert
            .Throws<ArgumentException>(() => EnrollementBuilder.New().SetCourse(course).SetPrice(course.Price).SetStudent(student).Build())
            .AssertExceptionWithMessageCheck(Globals.EnrollementCourseAndStudentHaveDifferentTargetAudiences);
    }

    [Theory(DisplayName = "ShouldNotCompleteEnrollementWithInvalidGrade")]
    [InlineData(-1)]
    [InlineData(-99)]
    [InlineData(11)]
    [InlineData(125)]
    public void ShouldNotCompleteEnrollementWithInvalidGrade(decimal invalidGrade)
    {

        Assert
            .Throws<ArgumentException>(() => EnrollementBuilder.New().Build().Complete(invalidGrade))
            .AssertExceptionWithMessageCheck(Globals.EnrollementWithInvalidGrade);
    }

    [Theory(DisplayName = "ShouldAcceptValidGrade")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(10)]
    public void ShouldAcceptValidGrade(decimal validGrade)
    {
        var enrollement = EnrollementBuilder.New().Build();
        enrollement.Complete(validGrade);

        Assert.Equal(validGrade, enrollement.Grade);
    }

    [Theory(DisplayName = "ShouldCompleteEnrollementWithValidGrade")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(10)]
    public void ShouldCompleteEnrollementWithValidGrade(decimal validGrade)
    {
        var enrollement = EnrollementBuilder.New().Build();
        enrollement.Complete(validGrade);

        Assert.True(enrollement.Completed);
    }

    [Fact(DisplayName = "ShouldCancelEnrollement")]
    public void ShouldCancelEnrollement()
    {
        var enrollement = EnrollementBuilder.New().Build();
        enrollement.Cancel();

        Assert.True(enrollement.Cancelled);
    }

    [Fact(DisplayName = "ShouldNotCancelEnrollementIfCompleted")]
    public void ShouldNotCancelEnrollementIfCompleted()
    {
        var enrollement = EnrollementBuilder.New().Build();
        enrollement.Complete(grade: 7);

        Assert
            .Throws<ArgumentException>(() => enrollement.Cancel())
            .AssertExceptionWithMessageCheck(Globals.EnrollementAlreadyCompletedCantCancelIt);
    }

    [Fact(DisplayName = "ShouldNotCompleteEnrollementIfCancelled")]
    public void ShouldNotCompleteEnrollementIfCancelled()
    {
        var enrollement = EnrollementBuilder.New().Build();
        enrollement.Cancel();

        Assert
            .Throws<ArgumentException>(() => enrollement.Complete(grade: 7))
            .AssertExceptionWithMessageCheck(Globals.EnrollementAlreadyCancelledCantCompleteIt);
    }
}
