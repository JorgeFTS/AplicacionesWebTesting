using ChambaPro.Platform.API.Review.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChambaPro.Platform.API.Review.Application.Queries;
using ChambaPro.Platform.API.Review.Domain.Models.Repositories;
using ChambaPro.Platform.API.Review.Domain.Services;

namespace ChambaPro.Platform.API.Review.Application.Internal.QueryServices
{
    public class ReviewQueryService : IReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewQueryService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Reviews>> Handle(GetReviewsByTechnicianIdQuery query)
        {
            var reviews = await _reviewRepository.FindByTechnicianIdAsync(query.TechnicianId);

            return reviews;
        }
    }
}