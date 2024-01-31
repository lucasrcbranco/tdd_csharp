using Domain.Models.Students;

namespace Domain.Tests.Students;

public interface IStudentRepository
{
    void Add(Student Student);
    Student? GetByEmail(string email);
    Student? GetByDocument(string document);
    Student? GetById(Guid id);
    void Update(Student Student);
}
