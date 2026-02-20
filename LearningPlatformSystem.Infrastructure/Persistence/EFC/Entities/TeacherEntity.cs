using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class TeacherEntity : EntityBase
{
    public Guid Id { get; set; }
    public PersonName Name { get; set; } = null!;
    public ContactInformation ContactInformation { get; set; } = null!;
    public Address? Address { get; set; }
}

