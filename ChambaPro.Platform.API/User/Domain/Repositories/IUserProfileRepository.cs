using System;
using System.Threading.Tasks;
using ChambaPro.Platform.API.User.Domain.Model.Aggregates;

namespace Chambapro.Platform.API.User.Domain.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> FindByUserIdAsync(Guid userId);
        Task AddAsync(UserProfile profile);
        Task UpdateAsync(UserProfile profile);
        Task DeleteAsync(UserProfile profile);
        Task SaveChangesAsync();
    }
}
