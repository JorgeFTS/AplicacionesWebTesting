namespace ChambaPro.Platform.API.Review.Interfaces.Rest.Resources
{
    public class ReviewResource
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string ClientName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}