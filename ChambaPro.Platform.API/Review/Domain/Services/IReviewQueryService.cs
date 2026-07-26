using ChambaPro.Platform.API.Review.Application.Queries;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;

namespace ChambaPro.Platform.API.Review.Domain.Services;

public interface  IReviewQueryService
{
    Task<IEnumerable<Reviews>> Handle(GetReviewsByTechnicianIdQuery query);
}