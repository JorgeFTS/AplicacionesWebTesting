namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

public record SignInResource(string Email, string Password, string UserType);