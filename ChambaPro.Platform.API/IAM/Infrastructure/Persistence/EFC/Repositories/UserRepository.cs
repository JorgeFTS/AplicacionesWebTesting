
using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Repositories;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<Users>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Users?> FindByIdAsync(int id)
    {
        return await Context.Set<Users>()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Users?> FindByEmailAsync(string email)
    {
        return await Context.Set<Users>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Users?> FindByEmailAndTypeAsync(string email, string type)
    {
        return await Context.Set<Users>()
            .FirstOrDefaultAsync(u => 
                u.Email == email && 
                u.Type.ToString().ToLower() == type.ToLower());
    }

    public async Task<IEnumerable<Users>> FindAllTechniciansAsync()
    {
        return await Context.Set<Users>()
            .Where(u => u.Type == Domain.Model.ValueObjects.UserType.Technician)
            .ToListAsync();
    }

    public async Task<IEnumerable<Users>> FindTechniciansBySpecialityAsync(string speciality)
    {
        return await Context.Set<Users>()
            .Where(u => 
                u.Type == Domain.Model.ValueObjects.UserType.Technician &&
                u.Speciality == speciality)
            .ToListAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Context.Set<Users>()
            .AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(Users user)
    {
        await Context.Set<Users>().AddAsync(user);
    }

    public void Update(Users user)
    {
        Context.Set<Users>().Update(user);
    }
}