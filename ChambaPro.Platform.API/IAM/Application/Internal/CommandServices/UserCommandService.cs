using System.Text.RegularExpressions;
using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Commands;
using ChambaPro.Platform.API.IAM.Domain.Model.ValueObjects;
using ChambaPro.Platform.API.IAM.Domain.Repositories;
using ChambaPro.Platform.API.IAM.Domain.Services;
using ChambaPro.Platform.API.Shared.Domain.Repositories;

namespace ChambaPro.Platform.API.IAM.Application.Internal.CommandServices;

public class UserCommandService : IUserCommandService
{
    
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashingService _hashingService;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IHashingService hashingService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _hashingService = hashingService;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<(Users?, string)> Handle(SignInCommand command)
    {
        var user = await _userRepository.FindByEmailAndTypeAsync(
            command.Email, 
            command.UserType
        );

        if (user is null)
            throw new Exception("User not found");

        if (!_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid password");

        var token = _tokenService.GenerateToken(user);

        return (user, token);
    }

    public async Task<Users?> Handle(SignUpCommand command)
    {
        
        if (!IsValidEmail(command.Email))
            throw new Exception("Invalid email format");
        
        if (await _userRepository.ExistsByEmailAsync(command.Email))
            throw new Exception("User with this email already exists");
        
        if (!IsValidPassword(command.Password))
            throw new Exception("Password must be at least 8 characters long and contain uppercase, lowercase, and numbers");

        var hashedPassword = _hashingService.HashPassword(command.Password);

        Users user;

        if (command.UserType.ToLower() == "technician")
        {
            if (!command.HourlyRate.HasValue)
                throw new Exception("Hourly rate is required for technicians");

            if (command.HourlyRate.Value < 10)
                throw new Exception("Hourly rate must be at least 10 soles");

            var validSpecialities = new[] { "Plumbing", "Electrical", "Carpentry", "Painting", "Locksmith" };
            if (!validSpecialities.Contains(command.Speciality))
                throw new Exception("Invalid speciality. Must be one of: Plumbing, Electrical, Carpentry, Painting, Locksmith");

            user = new Users(
                command.Email,
                hashedPassword,
                command.Name,
                command.Lastname,
                command.Phone,
                command.Speciality ?? string.Empty,
                command.Description ?? string.Empty,
                command.Experience ?? string.Empty,
                command.HourlyRate.Value
            );
        }
        else
        {

            if (command.Speciality != null || command.HourlyRate.HasValue)
                throw new Exception("Client users cannot have technician-specific data");
            
            user = new Users(
                command.Email,
                hashedPassword,
                command.Name,
                command.Lastname,
                command.Phone,
                UserType.Client
            );
        }

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }

    public async Task<Users?> Handle(UpdateProfileCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.UserId);
            
        if (user is null)
            throw new Exception("User not found");

        user.UpdateProfile(
            command.Name,
            command.Lastname,
            command.Phone,
            command.Avatar
        );

        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while updating profile: {e.Message}");
        }
    }

    public async Task<Users?> Handle(UpdateTechnicianProfileCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.UserId);
            
        if (user is null)
            throw new Exception("User not found");
        
        if (command.HourlyRate < 10 || command.HourlyRate > 500)
            throw new Exception("Hourly rate must be between 10 and 500 soles");
        
        if (string.IsNullOrWhiteSpace(command.Description) || command.Description.Length < 20)
            throw new Exception("Description must be at least 20 characters long");

        user.UpdateTechnicianProfile(
            command.Speciality,
            command.Description,
            command.Experience,
            command.HourlyRate,
            command.IsAvailable
        );

        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while updating technician profile: {e.Message}");
        }
    }
    
    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8 
               && password.Any(char.IsUpper) 
               && password.Any(char.IsLower) 
               && password.Any(char.IsDigit);
    }
    
}
