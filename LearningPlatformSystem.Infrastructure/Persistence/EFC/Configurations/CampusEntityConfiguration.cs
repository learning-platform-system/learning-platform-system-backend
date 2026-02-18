using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CampusEntityConfiguration : EntityBaseConfiguration<CampusEntity>
{
    public override void Configure(EntityTypeBuilder<CampusEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Campuses");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(Campus.CampusNameMaxLength);

        builder.OwnsOne(e => e.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasMaxLength(Address.StreetMaxLength);
            address.Property(a => a.PostalCode)
                .HasColumnName("PostalCode")
                .IsRequired()
                .HasMaxLength(Address.PostalCodeMaxLength);
            address.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(Address.CityMaxLength);
        });

        builder.OwnsOne(e => e.ContactInformation, contactInfo =>
        {
            contactInfo.Property(c => c.Email)
                .HasColumnName("Email")
                .HasMaxLength(ContactInformation.EmailMaxLength);
            contactInfo.Property(c => c.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(ContactInformation.PhoneNumberMaxLength);
        });

        // Index:
        builder.HasIndex(campus => campus.Name)
       .IsUnique();

        // Om email har ett värde måste den vara unik, om den är null - ignorera
        builder.HasIndex(campus => campus.ContactInformation.Email)
       .IsUnique()
       .HasFilter("[Email] IS NOT NULL");
    }
}

