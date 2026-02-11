using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class TeacherEntity : BaseEntity
{
    public Guid Id { get; set; }
    public PersonName Name { get; set; } = null!;
    public Guid ContactInformationId { get; set; }
    public ContactInformationEntity ContactInformation { get; set; } = null!;
}

/*
 Konfigurera relationen till ContactInformation:
builder.HasOne(t => t.ContactInformation)
       .WithOne() // eller .WithOne(ci => ci.Teacher) om du har back-navigation
       .HasForeignKey<TeacherEntity>(t => t.ContactInformationId);

PersonName mappas som ownedTypy:
builder.OwnsOne(t => t.Name, name =>
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