using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CoursePeriodEnrollments;

public class CoursePeriodEnrollment
{
    private CoursePeriodEnrollment() { } // parameterlös konstruktor som krävs av EF Core

    // 2 FK, Student x är inskriven i period y
    public Guid StudentId { get; private set; }
    public Guid CoursePeriodId { get; private set; }
    public Grade Grade { get; private set; }

    private CoursePeriodEnrollment(Guid studentId, Guid coursePeriodId, Grade grade)
    {
        StudentId = studentId;
        CoursePeriodId = coursePeriodId;
        Grade = grade;
    }

    internal static CoursePeriodEnrollment Create(Guid studentId, Guid coursePeriodId)
    {
        DomainValidator.ValidateRequiredGuid(studentId, CoursePeriodEnrollmentErrors.StudentIdIsRequired);
        DomainValidator.ValidateRequiredGuid(coursePeriodId, CoursePeriodEnrollmentErrors.CoursePeriodIdIsRequired);

        CoursePeriodEnrollment enrollment = new(studentId, coursePeriodId, Grade.NotSet);

        return enrollment;
    }

}
