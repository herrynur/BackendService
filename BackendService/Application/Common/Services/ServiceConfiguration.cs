
using BackendService.Application.PlaceApplication.Service;
using BackendService.Infrastructure.Database;

namespace BackendService.Application.Common.Services
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        private readonly ApplicationContext _context;
        public IPlaceService PlaceService { get; private set; }

        public ServiceConfiguration(ApplicationContext context)
        {
            _context = context;

            PlaceService = new PlaceService(_context);
        }
    }
}
