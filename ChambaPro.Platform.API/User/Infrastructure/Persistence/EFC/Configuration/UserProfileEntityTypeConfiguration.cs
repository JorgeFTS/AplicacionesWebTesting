using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChambaPro.Platform.API.User.Domain.Model.Aggregates;

namespace Chambapro.Platform.API.User.Infrastructure.Persistence.EFC.Configuration
{
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("user_profiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Address).HasMaxLength(500);
            builder.Property(x => x.Bio).HasMaxLength(1000);
            builder.Property(x => x.ProfilePictureUrl).HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
