namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class AddressEntity : EntityBase
{
    public Guid Id { get; set; }

    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
