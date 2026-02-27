using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CoursePeriodEnrollments;

public sealed class CoursePeriodEnrollment
{
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

    public void SetGrade(Grade grade)
    {
        if (Grade is not Grade.NotSet)
            throw new DomainException(CoursePeriodEnrollmentErrors.GradeAlreadySet);

        Grade = grade;
    }

    internal static CoursePeriodEnrollment Rehydrate(Guid studentId, Guid coursePeriodId, Grade grade)
    {
        return new CoursePeriodEnrollment(studentId, coursePeriodId, grade);

    }
}
