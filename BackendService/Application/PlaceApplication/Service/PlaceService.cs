using BackendService.Application.Common.Dtos;
using BackendService.Domain.Entities;
using BackendService.Helper.Responses;
using BackendService.Infrastructure.Database;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Application.PlaceApplication.Service
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationContext _context;

        public PlaceService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<PlacesReadDto>> GetAllPlaceAsync(CancellationToken cancellationToken)
        {
            var query = _context.Places
                .Where(e => e.IsDeleted == false)
                .AsQueryable();

            var places = await query.ToListAsync(cancellationToken);

            var placesDto = places.Adapt<List<PlacesReadDto>>();

            return placesDto;
        }

        public async Task<PlacesReadDto> GetSinglePlaceAsync(Guid id, CancellationToken cancellationToken)
        {
            var place = await _context.Places.FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false, cancellationToken);

            if (place is null)
            {
                throw new ApplicationException();
            }

            var result = place.Adapt<PlacesReadDto>();

            return result;
        }

        public async Task<ResponseBaseModel> PostPlaceAsync(PlacesWriteDto input, CancellationToken cancellationToken)
        {
            var response = new ResponseBaseModel();
            await using var transaction = _context.Database.BeginTransaction();

            try
            {
                Place place = new()
                {
                    OwnerName = input.OwnerName,
                    PlaceName = input.PlaceName,
                    Lattitude = input.Lattitude,
                    Longitude = input.Longitude,
                    Address = input.Address,
                };

                await _context.AddAsync(place, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                response.IsError = false;
                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                response.IsError = true;
                response.Message = ex.InnerException!.Message;
                return response;
            }
        }
    }
}
