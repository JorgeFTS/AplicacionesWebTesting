using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.IAM.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Users user);
    int? ValidateToken(string token);
}