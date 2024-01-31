using Domain._Utils;
using Domain.Common.Guards;
using Domain.Models.Students;

namespace Domain.Tests.Students;

public class StudentStorage : IStudentStorage
{
    private readonly IStudentRepository _repository;
    public StudentStorage(IStudentRepository repository)
    {
        _repository = repository;
    }

    public void Storage(StudentDto studentDto)
    {
        var studentByEmail = _repository.GetByEmail(studentDto.Email);
        Guards.Ensures(studentByEmail != null && studentByEmail.Id != studentDto.Id, Globals.StudentEmailAlreadyTaken);

        var studentByDocument = _repository.GetByDocument(studentDto.Document);
        Guards.Ensures(studentByDocument != null && studentByDocument.Id != studentDto.Id, Globals.StudentDocumentAlreadyTaken);

        if (studentDto.Id == null)
        {
            var student = new Student(
                studentDto.Name,
                studentDto.Document,
                studentDto.Email,
                studentDto.TargetAudience);

            _repository.Add(student);
        }
        else
        {
            var student = _repository.GetById(studentDto.Id.Value);
            Guards.Ensures(student == null, Globals.StudentNotFound(studentDto.Id.Value));

            student!.Update(studentDto.Name);

            _repository.Update(student);
        }
    }
}
