using Domain._Utils;
using Domain.Models.Courses;
using Domain.Models.Enrollements;
using Domain.Models.Students;
using Domain.Tests._Builders;
using Domain.Tests._Utils;
using Domain.Tests.Courses;
using Domain.Tests.Students;
using Moq;

namespace Domain.Tests.Enrollements;

public class EnrollementStorageTests
{
    private readonly EnrollementDto _enrollementDto;
    private readonly Mock<IEnrollementRepository> _mockRepository;
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly Mock<IStudentRepository> _mockStudentRepository;
    private readonly IEnrollementStorage _storage;
    private readonly Course _course;
    private readonly Student _student;

    public EnrollementStorageTests()
    {
        _mockRepository = new Mock<IEnrollementRepository>();
        _mockCourseRepository = new Mock<ICourseRepository>();
        _mockStudentRepository = new Mock<IStudentRepository>();

        _storage = new EnrollementStorage(_mockRepository.Object, _mockStudentRepository.Object, _mockCourseRepository.Object);

        _course = CourseBuilder.New().Build();
        _student = StudentBuilder.New().Build();

        _mockCourseRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(_course);
        _mockStudentRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(_student);

        _enrollementDto = new EnrollementDto
        {
            CourseId = _course.Id,
            StudentId = _student.Id,
            Price = _course.Price
        };
    }

    [Fact(DisplayName = "ShouldNotAddEnrollementWithInvalidCourse")]
    public void ShouldNotAddEnrollementWithInvalidCourse()
    {
        Course invalidCourse = null;
        _mockCourseRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(invalidCourse);

        Assert
            .Throws<ArgumentException>(() => _storage.Storage(_enrollementDto))
            .AssertExceptionWithMessageCheck(Globals.CourseNotFound(_enrollementDto.CourseId));
    }

    [Fact(DisplayName = "ShouldNotAddEnrollementWithInvalidStudent")]
    public void ShouldNotAddEnrollementWithInvalidStudent()
    {
        Student invalidCourse = null;
        _mockStudentRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(invalidCourse);

        Assert
            .Throws<ArgumentException>(() => _storage.Storage(_enrollementDto))
            .AssertExceptionWithMessageCheck(Globals.CourseNotFound(_enrollementDto.StudentId));
    }

    [Fact(DisplayName = "ShouldAddEnrollement")]
    public void ShouldAddEnrollement()
    {
        _storage.Storage(_enrollementDto);

        _mockRepository.Verify(r => r.Add(It.Is<Enrollement>(
            c => c.Course.Id == _enrollementDto.CourseId
                 && c.Student.Id == _enrollementDto.StudentId
                 && c.Price == _enrollementDto.Price)));
    }

    [Theory(DisplayName = "ShouldCompleteEnrollement")]
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
    public void ShouldCompleteEnrollement(decimal grade)
    {
        var enrollement = EnrollementBuilder.New().Build();
        _mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(enrollement);

        _storage.CompleteEnrollement(enrollement.Id, grade);

        Assert.True(enrollement.Completed);
    }

    [Theory(DisplayName = "ShouldNotCompleteEnrollementWithInvalidEnrollementId")]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    public void ShouldNotCompleteEnrollementWithInvalidEnrollementId(decimal grade)
    {
        var enrollementId = Guid.NewGuid();
        Enrollement invalidEnrollement = null;
        _mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(invalidEnrollement);

        Assert
            .Throws<ArgumentException>(() => _storage.CompleteEnrollement(enrollementId, grade))
            .AssertExceptionWithMessageCheck(Globals.EnrollementNotFound(enrollementId));
    }

    [Fact(DisplayName = "ShouldCancelEnrollement")]
    public void ShouldCancelEnrollement()
    {
        var enrollement = EnrollementBuilder.New().Build();
        _mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(enrollement);

        _storage.CancelEnrollement(enrollement.Id);

        Assert.True(enrollement.Cancelled);
    }

    [Fact(DisplayName = "ShouldNotCancelEnrollementWithInvalidEnrollementId")]
    public void ShouldNotCancelEnrollementWithInvalidEnrollementId()
    {
        var enrollementId = Guid.NewGuid();
        Enrollement invalidEnrollement = null;
        _mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(invalidEnrollement);

        Assert
            .Throws<ArgumentException>(() => _storage.CancelEnrollement(enrollementId))
            .AssertExceptionWithMessageCheck(Globals.EnrollementNotFound(enrollementId));
    }
}