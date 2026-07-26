using System;
using System.Threading.Tasks;
using ChambaPro.Platform.API.User.Domain.Model.Aggregates;
using Chambapro.Platform.API.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chambapro.Platform.API.User.Infrastructure.Persistence.EF
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly DbContext _context;

        public UserProfileRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile?> FindByUserIdAsync(Guid userId)
        {
            return await _context.Set<UserProfile>().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(UserProfile profile)
        {
            await _context.Set<UserProfile>().AddAsync(profile);
        }

        public async Task UpdateAsync(UserProfile profile)
        {
            _context.Set<UserProfile>().Update(profile);
        }

        public async Task DeleteAsync(UserProfile profile)
        {
            _context.Set<UserProfile>().Remove(profile);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
