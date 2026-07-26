using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using System.Collections.Generic;
using System.Linq;
using ChambaPro.Platform.API.Review.Application.Commands;

namespace ChambaPro.Platform.API.Review.Interfaces.Rest.Assemblers
{
    public static class ReviewResourceAssembler
    {
        public static SubmitReviewCommand ToCommandFromResource(CreateReviewResource resource)
        {
            return new SubmitReviewCommand
            {
                TechnicianId = resource.TechnicianId,
                ClientId = resource.ClientId,
                Rating = resource.Rating,
                Comment = resource.Comment
            };
        } 

        public static ReviewResource ToResourceFromEntity(Reviews entity)
        {
            return new ReviewResource
            {
                Id = entity.Id,
                Rating = entity.Rating,
                Comment = entity.Comment,
                CreationDate = entity.CreationDate,
                ClientName = "Nombre de Cliente Placeholder"
            };
        }

        public static IEnumerable<ReviewResource> ToResourceListFromEntityList(IEnumerable<Reviews> entityList)
        {
            return entityList.Select(ToResourceFromEntity);
        }
    }
}