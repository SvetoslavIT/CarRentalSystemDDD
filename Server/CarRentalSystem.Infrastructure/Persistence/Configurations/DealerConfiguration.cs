﻿namespace CarRentalSystem.Infrastructure.Persistence.Configurations
{
    using Domain.Models.Dealers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants;

    internal class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .HasMany(pr => pr.CarAds)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_carAds");

            builder
                .OwnsOne(d => d.PhoneNumber, p =>
                {
                    p.WithOwner();

                    p.Property(pn => pn.Number)
                        .IsRequired()
                        .HasMaxLength(PhoneNumberMaxLength);
                });
        }
    }
}