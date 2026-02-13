namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CampusEntity : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    // foreign key, kopplar campusentity till addressentity
    public Guid AddressId { get; set; }
    // navigation property, syns inte i databasen (för exempelvis, campus.Address.StreetName i repository)
    public AddressEntity Address { get; set; } = null!;
}
