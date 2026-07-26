namespace ChambaPro.Platform.API.Review.Application.Queries
{
    public class GetReviewsByTechnicianIdQuery
    {
        public int TechnicianId { get; set; }

        public GetReviewsByTechnicianIdQuery(int technicianId)
        {
            TechnicianId = technicianId;
        }
    }
}