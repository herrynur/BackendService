using FrontendService.Application.Models;
using FrontendService.Helpers;

namespace FrontendService.Application.Service
{
    public interface IPlaceServices
    {
        Task<List<PlaceReadDto>> GetAllPlaceAsync(CancellationToken cancellationToken);
        Task<ResponseBaseModel> PostPlaceAsync(PlaceWriteDto placeReadDto, CancellationToken cancellationToken);
        Task<PlaceReadDto> GetSinglePlaceAsync(Guid id, CancellationToken cancellationToken);
        Task<ResponseBaseModel> UpdatePlaceAsync(Guid id, PlaceReadDto input, CancellationToken cancellationToken);
        Task<ResponseBaseModel> DeletePlaceAsync(Guid id, CancellationToken cancellationToken);
    }
}
