using Domain._Utils;
using Domain.Common.Enums;
using Domain.Common.Guards;
using Domain.Models.Base;

namespace Domain.Models.Students;

public class Student : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Document { get; private set; } = null!;
    public TargetAudience TargetAudience { get; private set; }

    private Student()
    {

    }

    public Student(
        string name,
        string document,
        string email,
        TargetAudience targetAudience)
    {
        Guards.NotNullOrEmpty(name, Globals.StudentInvalidName);
        Guards.NotNullOrEmpty(email, Globals.StudentInvalidEmail);
        Guards.IsValidDocument(document, Globals.StudentInvalidDocument);

        Name = name;
        Document = document;
        Email = email;
        TargetAudience = targetAudience;
    }

    public void Update(string name)
    {
        Guards.NotNullOrEmpty(name, Globals.StudentInvalidName);

        Name = name;
    }
}
