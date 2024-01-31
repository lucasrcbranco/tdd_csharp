using Bogus;
using Bogus.Extensions.Brazil;
using Domain._Utils;
using Domain.Common.Enums;
using Domain.Models.Students;
using Domain.Tests._Builders;
using Domain.Tests._Utils;
using Moq;

namespace Domain.Tests.Students;

public class StudentStorageTests
{
    private readonly StudentDto _studentDto;
    private readonly Mock<IStudentRepository> _mockRepository;
    private readonly IStudentStorage _storage;

    public StudentStorageTests()
    {
        _mockRepository = new Mock<IStudentRepository>();
        _storage = new StudentStorage(_mockRepository.Object);

        var faker = new Faker();

        _studentDto = new StudentDto
        {
            Name = faker.Random.Words(3),
            Document = faker.Person.Cpf(false),
            Email = faker.Person.Email,
            TargetAudience = TargetAudience.Student
        };
    }

    [Fact(DisplayName = "ShouldAddStudent")]
    public void ShouldAddStudent()
    {
        _storage.Storage(_studentDto);

        _mockRepository.Verify(r => r.Add(It.Is<Student>(
            c => c.Name == _studentDto.Name
                 && c.Document == _studentDto.Document
                 && c.Email == _studentDto.Email
                 && c.TargetAudience == _studentDto.TargetAudience)));
    }

    [Fact(DisplayName = "ShouldUpdateStudent")]
    public void ShouldUpdateStudent()
    {
        _studentDto.Id = Guid.NewGuid();
        var student = StudentBuilder.New().Build();

        _mockRepository.Setup(r => r.GetById(_studentDto.Id.Value)).Returns(student);

        _storage.Storage(_studentDto);

        _mockRepository.Verify(r => r.Update(It.Is<Student>(c => c.Name == _studentDto.Name)));
    }

    [Fact(DisplayName = "ShouldNotAddTwoStudentsWithTheSameNameEmail")]
    public void ShouldNotAddTwoStudentsWithTheSameNameEmail()
    {
        var existingStudent = StudentBuilder.New().SetEmail(_studentDto.Email).Build();
        _mockRepository.Setup(r => r.GetByEmail(_studentDto.Email)).Returns(existingStudent);

        Assert
            .Throws<ArgumentException>(() => _storage.Storage(_studentDto))
            .AssertExceptionWithMessageCheck(Globals.StudentEmailAlreadyTaken);
    }

    [Fact(DisplayName = "ShouldNotAddTwoStudentsWithTheSameDocument")]
    public void ShouldNotAddTwoStudentsWithTheSameDocument()
    {
        var existingStudent = StudentBuilder.New().SetDocument(_studentDto.Document).Build();
        _mockRepository.Setup(r => r.GetByDocument(_studentDto.Document)).Returns(existingStudent);

        Assert
            .Throws<ArgumentException>(() => _storage.Storage(_studentDto))
            .AssertExceptionWithMessageCheck(Globals.StudentDocumentAlreadyTaken);
    }
}