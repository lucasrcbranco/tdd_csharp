using System.ComponentModel;

namespace Domain.Common.Enums;

public enum TargetAudience
{
    [Description("Student")]
    Student = 1,

    [Description("Employed")]
    Employed = 2,

    [Description("Bussiness")]
    Bussiness = 3
}