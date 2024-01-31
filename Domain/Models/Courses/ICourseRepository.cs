using Domain.Models.Courses;

namespace Domain.Tests.Courses;

public interface ICourseRepository
{
    void Add(Course course);
    void Update(Course course);
    Course? GetByName(string name);
    Course? GetById(Guid id);
}
