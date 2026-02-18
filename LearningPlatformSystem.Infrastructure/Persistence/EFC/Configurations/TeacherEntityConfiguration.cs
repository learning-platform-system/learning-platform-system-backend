using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class TeacherEntityConfiguration : EntityBaseConfiguration<TeacherEntity>
{
    public override void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        base.Configure(builder);
               
        builder.ToTable("Teachers");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
        .ValueGeneratedNever();

        //PersonName = ownedType (ägs av en annan entitet och lagras i samma tabell)
        builder.OwnsOne(teacher => teacher.Name, name =>
        {
            // Mappar value objectets egenskaper till kolumner i tabellen
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(PersonName.FirstNameMaxLength)
                .IsRequired();

            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(PersonName.LastNameMaxLength)
                .IsRequired();
        });

        builder.OwnsOne(teacher => teacher.ContactInformation, contactInfo =>
        {
            contactInfo.Property(c => c.Email)
                .HasColumnName("Email")
                .HasMaxLength(ContactInformation.EmailMaxLength)
                .IsRequired();
            contactInfo.Property(c => c.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(ContactInformation.PhoneNumberMaxLength)
                .IsRequired();
        });

        builder.OwnsOne(teacher => teacher.Address, address =>
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

        //Index:

        // databas-skydd mot dubletter
        builder.HasIndex(entity => entity.ContactInformation.Email)
       .IsUnique();
        // sökning på förnamn + efternamn
        builder.HasIndex(entity => new { entity.Name.FirstName, entity.Name.LastName });
    }
}
