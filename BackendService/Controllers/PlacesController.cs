using BackendService.Application.Common.Dtos;
using BackendService.Application.Common.Services;
using BackendService.Helper.Exceptions;
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
                throw new AppException(post.Message);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id,
            [FromBody] PlacesWriteDto input,
            CancellationToken cancellationToken)
        {
            var put = await service.PlaceService.PutPlaceAsync(id, input, cancellationToken);

            if (put.IsError)
            {
                throw new AppException(put.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var delete = await service.PlaceService.DeletePlaceAsync(id, cancellationToken);

            if (delete.IsError)
            {
                throw new AppException(delete.Message);
            }

            return Ok();
        }
    }
}
