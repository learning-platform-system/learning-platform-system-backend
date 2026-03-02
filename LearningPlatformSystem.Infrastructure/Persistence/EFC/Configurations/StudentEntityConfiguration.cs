using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class StudentEntityConfiguration : EntityBaseConfiguration<StudentEntity>
{
    public override void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Students");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
        .ValueGeneratedNever();

        builder.OwnsOne(student => student.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(PersonName.FirstNameMaxLength)
                .IsRequired();

            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(PersonName.LastNameMaxLength)
                .IsRequired();

            // sökning på förnamn + efternamn
            name.HasIndex(n => new { n.FirstName, n.LastName });
        });

        builder.OwnsOne(student => student.ContactInformation, contactInfo =>
        {
            contactInfo.Property(c => c.Email)
                .HasColumnName("Email")
                .HasMaxLength(ContactInformation.EmailMaxLength)
                .IsRequired();
            contactInfo.Property(c => c.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(ContactInformation.PhoneNumberMaxLength)
                .IsRequired();

            contactInfo.HasIndex(c => c.Email).IsUnique();
        });

        builder.OwnsOne(student => student.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .HasMaxLength(Address.StreetMaxLength);
            address.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(Address.CityMaxLength);
            address.Property(a => a.PostalCode)
                .HasColumnName("PostalCode")
                .HasMaxLength(Address.PostalCodeMaxLength);
        });
    }
}
