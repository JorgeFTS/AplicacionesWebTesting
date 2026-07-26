namespace ChambaPro.Platform.API.Review.Domain.Models.Aggregates
{
    public class Reviews
    {
        public int Id { get; private set; }
        public int TechnicianId { get; private set; }
        public int ClientId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreationDate { get; private set; }

        private Reviews() { }

        public Reviews(int technicianId, int clientId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("La puntuación debe estar entre 1 y 5.");

            TechnicianId = technicianId;
            ClientId = clientId;
            Rating = rating;
            Comment = comment;
            CreationDate = DateTime.UtcNow;
        }

        public void Update(int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("La puntuación debe estar entre 1 y 5.");

            Rating = rating;
            Comment = comment;
        }
    }
}