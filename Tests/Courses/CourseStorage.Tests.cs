using Bogus;
using Domain._Utils;
using Domain.Common.Enums;
using Domain.Models.Courses;
using Domain.Tests._Builders;
using Domain.Tests._Utils;
using Moq;

namespace Domain.Tests.Courses;

public class CourseStorageTests
{
    private readonly CourseDto _courseDto;
    private readonly Mock<ICourseRepository> _mockRepository;
    private readonly ICourseStorage _storage;

    public CourseStorageTests()
    {
        _mockRepository = new Mock<ICourseRepository>();
        _storage = new CourseStorage(_mockRepository.Object);

        var faker = new Faker();

        _courseDto = new CourseDto
        {
            Name = faker.Random.Words(3),
            Description = faker.Lorem.Paragraph(),
            Workload = faker.Random.Decimal(1, 100),
            TargetAudience = TargetAudience.Student,
            Price = faker.Random.Decimal(1, 1000)
        };
    }

    [Fact(DisplayName = "ShouldAddCourse")]
    public void ShouldAddCourse()
    {
        _storage.Storage(_courseDto);

        _mockRepository.Verify(r => r.Add(It.Is<Course>(
            c => c.Name == _courseDto.Name
                 && c.Description == _courseDto.Description
                 && c.Workload == _courseDto.Workload
                 && c.TargetAudience == _courseDto.TargetAudience
                 && c.Price == _courseDto.Price)));
    }

    [Fact(DisplayName = "ShouldUpdateCourse")]
    public void ShouldUpdateCourse()
    {
        _courseDto.Id = Guid.NewGuid();
        var course = CourseBuilder.New().Build();

        _mockRepository.Setup(r => r.GetById(_courseDto.Id.Value)).Returns(course);

        _storage.Storage(_courseDto);

        _mockRepository.Verify(r => r.Update(It.Is<Course>(
            c => c.Name == _courseDto.Name
                 && c.Description == _courseDto.Description
                 && c.Workload == _courseDto.Workload
                 && c.TargetAudience == _courseDto.TargetAudience
                 && c.Price == _courseDto.Price)));
    }

    [Fact(DisplayName = "ShouldNotAddTwoCoursesWithTheSameName")]
    public void ShouldNotAddTwoCoursesWithTheSameName()
    {
        var existingCourse = CourseBuilder.New().SetName(_courseDto.Name).Build();
        _mockRepository.Setup(r => r.GetByName(_courseDto.Name)).Returns(existingCourse);

        Assert
            .Throws<ArgumentException>(() => _storage.Storage(_courseDto))
            .AssertExceptionWithMessageCheck(Globals.CourseNameAlreadyTaken);
    }
}