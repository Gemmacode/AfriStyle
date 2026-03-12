using AfriStyle.Application.Commands.GetRecommendations;
using AfriStyle.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AfriStyle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecommendationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get hairstyle recommendations based on face measurements and preferences.
        /// </summary>
        /// <param name="request">Face measurements from MediaPipe + user preferences</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Face shape analysis + top 8 recommended hairstyles</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RecommendationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRecommendations(
            [FromBody] RecommendationRequestDto request,
            CancellationToken cancellationToken)
        {
            var command = new GetRecommendationsCommand(request);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
