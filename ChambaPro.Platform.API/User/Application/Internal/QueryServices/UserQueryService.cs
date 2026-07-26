using Chambapro.Platform.API.User.Application.DTOs;
using Chambapro.Platform.API.User.Domain.Repositories;

namespace Chambapro.Platform.API.User.Application.Internal.QueryServices
{
    public class UserQueryService
    {
        private readonly IUserProfileRepository _repository;

        public UserQueryService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileResource?> HandleAsync(Guid userId)
        {
            var profile = await _repository.FindByUserIdAsync(userId);
            if (profile == null) return null;

            return new UserProfileResource
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FullName = profile.FullName,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                Bio = profile.Bio,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
        }
    }
}
