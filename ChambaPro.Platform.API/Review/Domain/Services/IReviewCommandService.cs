using ChambaPro.Platform.API.Review.Application.Commands;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;

namespace ChambaPro.Platform.API.Review.Domain.Services;

public interface IReviewCommandService
{
    
    Task<Reviews?> Handle(SubmitReviewCommand command);
}