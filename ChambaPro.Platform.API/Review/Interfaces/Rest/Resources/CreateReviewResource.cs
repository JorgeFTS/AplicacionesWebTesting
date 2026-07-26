namespace ChambaPro.Platform.API.Review.Interfaces.Rest.Resources
{
    public class CreateReviewResource
    {
        public int TechnicianId { get; set; }
        public int ClientId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}