using ChambaPro.Platform.API.IAM.Domain.Services;

namespace ChambaPro.Platform.API.IAM.Application.Internal.OutboundServices;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch
        {
            return false;
        }
    }
}