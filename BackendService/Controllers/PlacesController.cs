using BackendService.Application.Common.Dtos;
using BackendService.Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [ApiController]
    public class PlacesController(IServiceConfiguration service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PlacesReadDto>> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await service.PlaceService.GetAllPlaceAsync(cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] PlacesWriteDto input,
            CancellationToken cancellationToken)
        {
            var post = await service.PlaceService.PostPlaceAsync(input, cancellationToken);

            if (post.IsError)
            {
                throw new ApplicationException(post.Message);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlacesReadDto>> GetSingle(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var result = await service.PlaceService.GetSinglePlaceAsync(id, cancellationToken);

            return Ok(result);
        }
    }
}
