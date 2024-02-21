using BackendService.Application.PlaceApplication.Service;

namespace BackendService.Application.Common.Services
{
    public interface IServiceConfiguration
    {
        IPlaceService PlaceService { get; }
    }
}
