using Domain._Utils;
using Domain.Common.Guards;
using Domain.Models.Enrollements;
using Domain.Tests.Courses;
using Domain.Tests.Students;

namespace Domain.Tests.Enrollements;

public class EnrollementStorage : IEnrollementStorage
{
    private readonly IEnrollementRepository _repository;
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;
    public EnrollementStorage(
        IEnrollementRepository repository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository)
    {
        _repository = repository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    public void Storage(EnrollementDto enrollementDto)
    {
        var course = _courseRepository.GetById(enrollementDto.CourseId);
        var student = _studentRepository.GetById(enrollementDto.StudentId);

        Guards.Ensures(course == null, Globals.CourseNotFound(enrollementDto.CourseId));
        Guards.Ensures(student == null, Globals.CourseNotFound(enrollementDto.StudentId));

        var enrollement = new Enrollement(course!, student!, enrollementDto.Price);
        _repository.Add(enrollement);
    }

    public void CompleteEnrollement(Guid enrollementId, decimal grade)
    {
        var enrollement = _repository.GetById(enrollementId);
        Guards.Ensures(enrollement == null, Globals.EnrollementNotFound(enrollementId));

        enrollement!.Complete(grade);
        _repository.Update(enrollement!);
    }

    public void CancelEnrollement(Guid enrollementId)
    {
        var enrollement = _repository.GetById(enrollementId);
        Guards.Ensures(enrollement == null, Globals.EnrollementNotFound(enrollementId));

        enrollement!.Cancel();
        _repository.Update(enrollement!);
    }
}
