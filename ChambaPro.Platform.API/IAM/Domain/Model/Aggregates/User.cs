using System.Text.Json.Serialization;
using ChambaPro.Platform.API.IAM.Domain.Model.ValueObjects;

namespace ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;

public class Users
{
    public int Id { get; }
    public string Email {get; private set;}
    public string PasswordHash {get; private set;}
    public string Name {get; private set;}
    public string LastName {get; private set;}
    public string Phone {get; private set;}
    public string Avatar {get; private set;}
    public UserType Type {get; private set;}
    public DateTime CreatedAt {get;}
    public string? Speciality {get; private set;}
    public string? Description {get; private set;}
    public string? Experience {get; private set;}
    public decimal? Rating {get; private set;}
    public int? ReviewsCount {get; private set;}
    public decimal? HourlyRate {get; private set;}
    public bool? IsAvailable {get; private set;}

    protected Users()
    {
        Email = string.Empty;
        PasswordHash = string.Empty;
        Name = string.Empty;
        LastName = string.Empty;
        Phone = string.Empty;
        Avatar = string.Empty;
    }
    
    public Users(string email, string passwordHash, string name, string lastName, string phone, UserType type)
    {
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        LastName = lastName;
        Phone = phone;
        Type = type;
        Avatar = $"/avatars/default-{type.ToString().ToLower()}.png";
        CreatedAt = DateTime.UtcNow;
    }

    public Users(string email, string passwordHash, string name, string lastname,
        string phone, string speciality, string description, string experience,
        decimal hourlyRate) : this(email, passwordHash, name, lastname, phone, UserType.Technician)
    {
        Speciality = speciality;
        Description = description;
        Experience = experience;
        Rating = 0;
        ReviewsCount = 0;
        HourlyRate = hourlyRate;
        IsAvailable = true;
    }
    
    public Users UpdateProfile(string name, string lastname, string phone, string? avatar = null)
    {
        Name = name;
        LastName = lastname;
        Phone = phone;
        if (!string.IsNullOrEmpty(avatar))
            Avatar = avatar;
        return this;
    }
    
    public Users UpdateTechnicianProfile(string speciality, string description, 
        string experience, decimal hourlyRate, bool isAvailable)
    {
        if (Type != UserType.Technician)
            throw new InvalidOperationException("Only technicians can update technician profile");

        Speciality = speciality;
        Description = description;
        Experience = experience;
        HourlyRate = hourlyRate;
        IsAvailable = isAvailable;
        return this;
    }
    
    public Users UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        return this;
    }
    
    public Users UpdateRating(decimal newRating, int newReviewsCount)
    {
        if (Type != UserType.Technician)
            throw new InvalidOperationException("Only technicians have ratings");

        Rating = newRating;
        ReviewsCount = newReviewsCount;
        return this;
    }
}