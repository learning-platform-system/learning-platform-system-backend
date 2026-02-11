using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class StudentEntity : BaseEntity
{
    public Guid Id { get; set; }
    public PersonName Name { get; set; } = null!;

    public Guid ContactInformationId { get; set; }
    public ContactInformationEntity ContactInformation { get; set; } = null!;
}

/*
PersonName Mappning i fluent api:
builder.OwnsOne(s => s.Name, name =>
{
    name.Property(n => n.FirstName)
        .HasColumnName("FirstName")
        .HasMaxLength(50)
        .IsRequired();

    name.Property(n => n.LastName)
        .HasColumnName("LastName")
        .HasMaxLength(50)
        .IsRequired();
});
*/