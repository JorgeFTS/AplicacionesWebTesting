namespace ChambaPro.Platform.API.Review.Application.Commands
{
    public class SubmitReviewCommand
    {
        public int TechnicianId { get; set; }

        public int ClientId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}