using Domain.Common.Enums;

namespace Domain.Tests.Students;

public class StudentDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Document { get; set; } = null!;
    public TargetAudience TargetAudience { get; set; }
}