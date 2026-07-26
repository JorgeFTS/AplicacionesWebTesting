using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Threading.Tasks;
using System;
using ChambaPro.Platform.API.Review.Application.Commands;
using ChambaPro.Platform.API.Review.Domain.Models.Repositories;
using ChambaPro.Platform.API.Review.Domain.Services;

namespace ChambaPro.Platform.API.Review.Application.Internal.CommandServices
{
    public class ReviewCommandService : IReviewCommandService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewCommandService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Reviews?> Handle(SubmitReviewCommand command)
        {
            if (command.Rating < 1 || command.Rating > 5)
            {
                return null;
            }

            var newReview = new Reviews(
                command.TechnicianId,
                command.ClientId,
                command.Rating,
                command.Comment
            );

            await _reviewRepository.AddAsync(newReview);

            return newReview;
        }
    }
}