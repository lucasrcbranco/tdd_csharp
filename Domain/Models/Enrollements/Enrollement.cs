using Domain._Utils;
using Domain.Common.Guards;
using Domain.Models.Base;
using Domain.Models.Courses;
using Domain.Models.Students;

namespace Domain.Models.Enrollements;

public class Enrollement : BaseEntity
{

    public Course Course { get; private set; }
    public Student Student { get; private set; }
    public decimal Price { get; private set; }
    public bool WasDiscounted { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Grade { get; private set; }
    public bool Completed { get; private set; }
    public bool Cancelled { get; private set; }

    private Enrollement()
    {

    }

    public Enrollement(Course course, Student student, decimal price)
    {
        Guards.Ensures(course == null, Globals.EnrollementWithInvalidCourse);
        Guards.Ensures(student == null, Globals.EnrollementWithInvalidStudent);
        Guards.Ensures(price < 1, Globals.EnrollementWithInvalidPrice);
        Guards.Ensures(course != null && price > course.Price, Globals.EnrollementWithPriceGreaterThanCoursePrice);
        Guards.Ensures(course != null
                        && student != null
                        && course.TargetAudience != student.TargetAudience, Globals.EnrollementCourseAndStudentHaveDifferentTargetAudiences);


        Course = course!;
        Student = student!;
        Price = price;

        WasDiscounted = course!.Price > price;
        Discount = course!.Price - price;
    }

    public void Complete(decimal grade)
    {
        Guards.Ensures(Cancelled, Globals.EnrollementAlreadyCancelledCantCompleteIt);
        Guards.Ensures(grade < 0 || grade > 10, Globals.EnrollementWithInvalidGrade);

        Grade = grade;
        Completed = true;
    }

    public void Cancel()
    {
        Guards.Ensures(Completed, Globals.EnrollementAlreadyCompletedCantCancelIt);

        Cancelled = true;
    }
}
