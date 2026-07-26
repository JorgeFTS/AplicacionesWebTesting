namespace ChambaPro.Platform.API.IAM.Domain.Model.Queries;

public record GetUserByEmailAndTypeQuery(string Email, string UserType);