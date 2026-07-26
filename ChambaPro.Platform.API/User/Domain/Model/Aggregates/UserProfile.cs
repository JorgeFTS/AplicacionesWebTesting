using System;

namespace ChambaPro.Platform.API.User.Domain.Model.Aggregates
{
    public class UserProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public string Bio { get; private set; } = string.Empty;
        public string? ProfilePictureUrl { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        protected UserProfile() { }

        public UserProfile(Guid userId, string fullName, string phoneNumber, string address, string bio)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Address = address;
            Bio = bio;
        }

        public void UpdateProfile(string fullName, string phoneNumber, string address, string bio)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Address = address;
            Bio = bio;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetProfilePicture(string url)
        {
            ProfilePictureUrl = url;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
