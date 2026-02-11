namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class AddressEntity : BaseEntity
{
    public Guid Id { get; set; }

    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
