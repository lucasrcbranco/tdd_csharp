using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Common.Enums;
using Domain.Models.Students;

namespace Domain.Tests._Builders;

public class StudentBuilder
{
    private string _name;
    private string _email;
    private string _document;
    private TargetAudience _targetAudience;

    public StudentBuilder()
    {
        var faker = new Faker();

        _name = faker.Person.FullName;
        _email = faker.Person.Email;
        _document = faker.Person.Cpf();

        _targetAudience = TargetAudience.Student;
    }

    public static StudentBuilder New() => new();

    public StudentBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public StudentBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public StudentBuilder SetDocument(string document)
    {
        _document = document;
        return this;
    }

    public StudentBuilder SetTargetAudience(TargetAudience targetAudience)
    {
        _targetAudience = targetAudience;
        return this;
    }

    public Student Build() => new(_name, _document, _email, _targetAudience);
}
