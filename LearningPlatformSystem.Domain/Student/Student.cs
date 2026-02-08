using LearningPlatformSystem.Domain.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

namespace LearningPlatformSystem.Domain.Student;

public class Student
{
    public Guid Id { get; private set; }
    public PersonName Name { get; private set; } = null!;
    public ContactInformation ContactInformation { get; private set; } = null!;
    //public List<CoursePeriodEnrollment>? CoursePeriodEnrollments { get; private set; }

    private Student(Guid id, PersonName name, ContactInformation contactInformation)
    {
        Id = id;
        Name = name;
        ContactInformation = contactInformation;
    }

    public static Student Create(string firstName, string lastName, string email, string phoneNumber)
    {
        PersonName name = PersonName.Create(firstName, lastName);

        ContactInformation contactInformation = ContactInformation.Create(email, phoneNumber);

        Guid id = Guid.NewGuid();
        Student student = new(id, name, contactInformation);

        return student;
    }

    //public void EnrollInCoursePeriod(CoursePeriod coursePeriod)
    //{
    //    if (CoursePeriodEnrollments == null)
    //    {
    //        CoursePeriodEnrollments = new List<CoursePeriodEnrollment>();
    //    }
    //    CoursePeriodEnrollment enrollment = CoursePeriodEnrollment.Create(this, coursePeriod);
    //    CoursePeriodEnrollments.Add(enrollment);
    //}
}
