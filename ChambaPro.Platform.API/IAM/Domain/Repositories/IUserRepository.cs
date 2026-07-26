using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.IAM.Domain.Repositories;

public interface IUserRepository
{
    Task<Users?> FindByIdAsync(int id);
    Task<Users?> FindByEmailAsync(string email);
    Task<Users?> FindByEmailAndTypeAsync(string email, string type);
    Task<IEnumerable<Users>> FindAllTechniciansAsync();
    Task<IEnumerable<Users>> FindTechniciansBySpecialityAsync(string speciality);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(Users user);
    void Update(Users user);
}