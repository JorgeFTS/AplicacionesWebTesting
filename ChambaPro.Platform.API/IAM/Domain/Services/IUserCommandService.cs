using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Commands;

namespace ChambaPro.Platform.API.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(Users?, string)> Handle(SignInCommand command);
    Task<Users?> Handle(SignUpCommand command);
    Task<Users?> Handle(UpdateProfileCommand command);
    Task<Users?> Handle(UpdateTechnicianProfileCommand command);
}