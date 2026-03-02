using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CampusEntity : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Address Address { get; set; } = null!;
    // Navigeringsegenskap som används av EF Core för att representera relationen (.HasOne()) och möjliggöra laddning av relaterad data. I repot: .Include(campus => campus.ContactInformation) --> möjligt att skriva campus.ContactInformation.PhoneNumber
    public ContactInformation? ContactInformation { get; set; }
}
