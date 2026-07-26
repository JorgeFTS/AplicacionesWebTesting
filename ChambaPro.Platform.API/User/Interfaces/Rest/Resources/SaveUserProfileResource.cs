namespace Chambapro.Platform.API.User.Interfaces.Rest.Resources
{
    public class SaveUserProfileResource
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }
}
