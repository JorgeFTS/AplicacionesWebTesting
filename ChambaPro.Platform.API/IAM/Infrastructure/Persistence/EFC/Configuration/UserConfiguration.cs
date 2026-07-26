using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChambaPro.Platform.API.IAM.Infrastructure.Persistence.EFC.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.HasIndex(u => u.Email).IsUnique();
            
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(u => u.Phone)
            .HasMaxLength(20);
            
        builder.Property(u => u.Avatar)
            .HasMaxLength(200);
            
        builder.Property(u => u.Type)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(u => u.CreatedAt)
            .IsRequired();
            
        builder.Property(u => u.Speciality)
            .HasMaxLength(50);
            
        builder.Property(u => u.Description)
            .HasMaxLength(500);
            
        builder.Property(u => u.Experience)
            .HasMaxLength(50);
            
        builder.Property(u => u.Rating)
            .HasColumnType("decimal(3,2)");
            
        builder.Property(u => u.ReviewsCount);
            
        builder.Property(u => u.HourlyRate)
            .HasColumnType("decimal(10,2)");
            
        builder.Property(u => u.IsAvailable);
    }
}