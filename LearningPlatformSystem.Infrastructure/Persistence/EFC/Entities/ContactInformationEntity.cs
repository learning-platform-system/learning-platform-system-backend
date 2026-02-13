using LearningPlatformSystem.Domain.Addresses;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class ContactInformationEntity : EntityBase
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Guid AddressId { get; set; }
    public AddressEntity? Address { get; set; }
}
