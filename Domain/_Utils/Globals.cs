namespace Domain._Utils;

public static class Globals
{
    public static string CourseInvalidName = "Course.Name must have a value that is not null or empty";
    public static string CourseNameAlreadyTaken = "Course.Name is already taken";

    public static string CourseInvalidDescription = "Course.Description must have a value that is not null or empty";
    public static string CourseInvalidWorkload = "Course.Workload must have a value greater than zero";
    public static string CourseInvalidPrice = "Course.Price must have a value greater than zero";

    public static string StudentInvalidName = "Student.Name must have a value that is not null or empty";
    public static string StudentNameAlreadyTaken = "Student.Name is already taken";

    public static string StudentInvalidEmail = "Student.Email must have a value that is not null or empty";
    public static string StudentEmailAlreadyTaken = "Student.Email is already taken";

    public static string StudentInvalidDocument = "Student.Document must have exactly 11 digits";
    public static string StudentDocumentAlreadyTaken = "Student.Document is already taken";

    public static string EnrollementWithInvalidStudent = "Enrollement.Student must not be null";
    public static string EnrollementWithInvalidCourse = "Enrollement.Course must not be null";
    public static string EnrollementWithInvalidPrice = "Enrollement.Price must have a value greater than 0";
    public static string EnrollementWithPriceGreaterThanCoursePrice = "Enrollement.Price must not be greater than the course price";
    public static string EnrollementCourseAndStudentHaveDifferentTargetAudiences = "Enrollement.Course and Enrollement.Student must have the same target audience";

    public static string EnrollementWithInvalidGrade = "Enrollement.Grade must not be greater than 11 or less than 0";

    public static string EnrollementAlreadyCompletedCantCancelIt = "Enrollement.Cancelled cannot be true if Enrollement.Completed is already true";
    public static string EnrollementAlreadyCancelledCantCompleteIt = "Enrollement.Completed cannot be true if Enrollement.Cancelled is already true";

    public static string CourseNotFound(Guid id) => $"Course.Id {id} could not be found";
    public static string StudentNotFound(Guid id) => $"Student.Id {id} could not be found";
    public static string EnrollementNotFound(Guid id) => $"Enrollement.Id {id} could not be found";
}
