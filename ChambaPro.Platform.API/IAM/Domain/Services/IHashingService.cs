namespace ChambaPro.Platform.API.IAM.Domain.Services;

public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}