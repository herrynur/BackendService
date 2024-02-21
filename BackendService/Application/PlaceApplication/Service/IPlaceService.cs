using BackendService.Application.Common.Dtos;
using BackendService.Helper.Responses;

namespace BackendService.Application.PlaceApplication.Service
{
    public interface IPlaceService
    {
        Task<List<PlacesReadDto>> GetAllPlaceAsync(CancellationToken cancellationToken);
        Task<PlacesReadDto> GetSinglePlaceAsync(Guid id, CancellationToken cancellationToken);
        Task<ResponseBaseModel> PostPlaceAsync(PlacesWriteDto input, CancellationToken cancellationToken);
    }
}
