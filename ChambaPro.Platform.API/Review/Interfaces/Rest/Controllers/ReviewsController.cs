using ChambaPro.Platform.API.Review.Application.Commands;
using ChambaPro.Platform.API.Review.Application.Queries;
using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Review.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Assemblers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Mime;
using ChambaPro.Platform.API.Review.Domain.Services;
using Microsoft.Extensions.Localization;

namespace ChambaPro.Platform.API.Review.Interfaces.Rest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewCommandService _commandService;
        private readonly IReviewQueryService _queryService;
        private readonly IStringLocalizer _localizer;

        public ReviewsController(
            IReviewCommandService commandService,
            IReviewQueryService queryService,
            IStringLocalizerFactory localizerFactory)
        {
            _commandService = commandService;
            _queryService = queryService;
            _localizer = localizerFactory.Create("ReviewsController", typeof(ReviewsController).Assembly.GetName().Name!);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewResource), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> SubmitReview([FromBody] CreateReviewResource resource)
        {
            var command = ReviewResourceAssembler.ToCommandFromResource(resource);
            var result = await _commandService.Handle(command);

            if (result == null)
            {
                return BadRequest(new { message = _localizer["Review_Creation_Failed"] });
            }

            return StatusCode(201, new { message = _localizer["Review_Creation_Success"] });
        }

        [HttpGet("technician/{technicianId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewResource>), 200)]
        public async Task<IActionResult> GetReviewsByTechnicianId(int technicianId)
        {
            try
            {
                var query = new GetReviewsByTechnicianIdQuery(technicianId);
                var reviews = await _queryService.Handle(query);
                var reviewResources = ReviewResourceAssembler.ToResourceListFromEntityList(reviews);

                return Ok(new
                {
                    message = _localizer["Reviews_Fetched"],
                    data = reviewResources
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
