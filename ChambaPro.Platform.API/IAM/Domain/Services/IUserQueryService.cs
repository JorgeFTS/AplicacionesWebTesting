using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Queries;

namespace ChambaPro.Platform.API.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<Users?> Handle(GetUserByIdQuery query);
    Task<Users?> Handle(GetUserByEmailQuery query);
    Task<Users?> Handle(GetUserByEmailAndTypeQuery query);
    Task<IEnumerable<Users>> Handle(GetAllTechniciansQuery query);
    Task<IEnumerable<Users>> Handle(GetTechniciansBySpecialityQuery query);
}