using Domain._Utils;
using Domain.Common.Guards;
using Domain.Models.Courses;

namespace Domain.Tests.Courses;

public class CourseStorage : ICourseStorage
{
    private readonly ICourseRepository _repository;
    public CourseStorage(ICourseRepository repository)
    {
        _repository = repository;
    }

    public void Storage(CourseDto courseDto)
    {
        var courseByName = _repository.GetByName(courseDto.Name);
        Guards.Ensures(courseByName != null && courseByName.Id != courseDto.Id, Globals.CourseNameAlreadyTaken);

        if (courseDto.Id == null)
        {
            var course = new Course(
                courseDto.Name,
                courseDto.Description,
                courseDto.Workload,
                courseDto.TargetAudience,
                courseDto.Price);

            _repository.Add(course);
        }
        else
        {
            var course = _repository.GetById(courseDto.Id.Value);
            Guards.Ensures(course == null, Globals.CourseNotFound(courseDto.Id.Value));

            course!.Update(
                courseDto.Name,
                courseDto.Description,
                courseDto.Workload,
                courseDto.TargetAudience,
                courseDto.Price);

            _repository.Update(course);
        }
    }
}
