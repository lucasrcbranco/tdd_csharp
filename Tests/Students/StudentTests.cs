using Bogus;
using Bogus.Extensions.Brazil;
using Domain._Utils;
using Domain.Common.Enums;
using Domain.Models.Students;
using Domain.Tests._Builders;
using Domain.Tests._Utils;
using ExpectedObjects;

namespace Domain.Tests.Students;

/// <summary>
/// USER STORY:
/// Eu, enquanto administrador, quero cadastrar um aluno com nome, cpf, e-mail, público alvo para poder efetuar sua matrícula.
/// Eu, enquanto administrador, quero editar somente o nome do aluno para poder corrigi-lo em caso de erro.
/// Eu, enquanto administrador, quero cancelar a matrícula do aluno para que o mesmo não faça mais parte do curso.
///     - A matrícula deve ser cancelada
///     - Não pode quando curso já estiver concluído
/// </summary>
public class StudentTests
{
    private readonly string _name;
    private readonly string _email;
    private readonly TargetAudience _targetAudience;
    private readonly string _document;

    public StudentTests()
    {
        var faker = new Faker();

        _name = faker.Person.FullName;
        _email = faker.Person.Email;
        _document = faker.Person.Cpf(false);

        _targetAudience = TargetAudience.Student;
    }

    [Fact(DisplayName = "ShouldCreateStudent")]
    public void ShouldCreateStudent()
    {
        // Arrange
        var expectedStudent = new
        {
            Name = _name,
            Email = _email,
            Document = _document,
            TargetAudience = _targetAudience
        };

        // Action
        var student = new Student(
            expectedStudent.Name,
            expectedStudent.Document,
            expectedStudent.Email,
            expectedStudent.TargetAudience);

        // Assert
        expectedStudent.ToExpectedObject().ShouldMatch(student);
    }

    [Theory(DisplayName = "ShouldNotCreateStudentWithInvalidName")]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldNotCreateStudentWithInvalidName(string invalidName)
    {
        Assert
            .Throws<ArgumentException>(() => StudentBuilder.New().SetName(invalidName).Build())
            .AssertExceptionWithMessageCheck(Globals.StudentInvalidName);
    }

    [Theory(DisplayName = "ShouldNotCreateStudentWithInvalidEmail")]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldNotCreateStudentWithInvalidEmail(string invalidEmail)
    {
        Assert
            .Throws<ArgumentException>(() => StudentBuilder.New().SetEmail(invalidEmail).Build())
            .AssertExceptionWithMessageCheck(Globals.StudentInvalidEmail);
    }

    [Theory(DisplayName = "ShouldNotCreateStudentWithInvalidDocument")]
    [InlineData("000")]
    [InlineData("000.000-56")]
    [InlineData("000.000.000.000")]
    public void ShouldNotCreateStudentWithInvalidDocument(string invalidDocument)
    {
        Assert
            .Throws<ArgumentException>(() => StudentBuilder.New().SetDocument(invalidDocument).Build())
            .AssertExceptionWithMessageCheck(Globals.StudentInvalidDocument);

    }
}
