using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChambaPro.Platform.API.Review.Infrastructure.Persistence.EFC.Configuration
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(EntityTypeBuilder<Reviews> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.Property(r => r.TechnicianId)
                   .IsRequired();

            builder.Property(r => r.ClientId)
                   .IsRequired();

            builder.Property(r => r.CreationDate)
                   .IsRequired();
        }
    }
}