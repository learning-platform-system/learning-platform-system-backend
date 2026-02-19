using LearningPlatformSystem.Domain.CoursePeriodEnrollments;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CoursePeriodEnrollmentEntity : EntityBase
{
    public Guid CoursePeriodId { get; set; }
    public CoursePeriodEntity CoursePeriod { get; set; } = null!;
    public Guid StudentId { get; set; }
    public StudentEntity Student { get; set; } = null!;
    public Grade Grade { get; set; }
}
/* 
Är inte en vanlig kopplingstabell iom att den har en extra egenskap (Grade) och därför behöver vara en egen entity.
Om det inte hade funnits någon extra egenskap hade det räckt med en vanlig många-till-många-relation utan en explicit entity.
*/