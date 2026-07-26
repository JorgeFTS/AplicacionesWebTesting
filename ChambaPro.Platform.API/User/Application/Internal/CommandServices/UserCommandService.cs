
using Chambapro.Platform.API.User.Application.DTOs;
using ChambaPro.Platform.API.User.Domain.Model.Aggregates;
using Chambapro.Platform.API.User.Domain.Repositories;
using Chambapro.Platform.API.User.Interfaces.Rest.Resources;

namespace Chambapro.Platform.API.User.Application.Internal.CommandServices
{
    public class UserCommandService
    {
        private readonly IUserProfileRepository _repository;

        public UserCommandService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileResource> HandleCreateOrUpdateAsync(Guid userId, SaveUserProfileResource resource)
        {
            var existing = await _repository.FindByUserIdAsync(userId);
            if (existing == null)
            {
                var newProfile = new UserProfile(userId, resource.FullName, resource.PhoneNumber, resource.Address, resource.Bio);
                await _repository.AddAsync(newProfile);
                await _repository.SaveChangesAsync();
                return new UserProfileResource {
                    Id = newProfile.Id,
                    UserId = newProfile.UserId,
                    FullName = newProfile.FullName,
                    PhoneNumber = newProfile.PhoneNumber,
                    Address = newProfile.Address,
                    Bio = newProfile.Bio,
                    ProfilePictureUrl = newProfile.ProfilePictureUrl,
                    CreatedAt = newProfile.CreatedAt,
                    UpdatedAt = newProfile.UpdatedAt
                };
            }

            existing.UpdateProfile(resource.FullName, resource.PhoneNumber, resource.Address, resource.Bio);
            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return new UserProfileResource {
                Id = existing.Id,
                UserId = existing.UserId,
                FullName = existing.FullName,
                PhoneNumber = existing.PhoneNumber,
                Address = existing.Address,
                Bio = existing.Bio,
                ProfilePictureUrl = existing.ProfilePictureUrl,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }
    }
}
