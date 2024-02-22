using BackendService.Application.Common.Dtos;
using BackendService.Domain.Entities;
using BackendService.Helper.Exceptions;
using BackendService.Helper.Responses;
using BackendService.Infrastructure.Database;
using Mapster;
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
        private IQueryable<Place> PlaceQuery()
        {
            var query = _context.Places
                .Where(e => e.IsDeleted == false && e.IsActive == true)
                .AsQueryable();

            return query;
        }

        public async Task<List<PlacesReadDto>> GetAllPlaceAsync(CancellationToken cancellationToken)
        {
            var places = await PlaceQuery().ToListAsync(cancellationToken);

            var placesDto = places.Adapt<List<PlacesReadDto>>();

            return placesDto;
        }

        public async Task<PlacesReadDto> GetSinglePlaceAsync(Guid id, CancellationToken cancellationToken)
        {
            var place = await PlaceQuery().FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false, cancellationToken);

            if (place is null)
            {
                throw new NotFoundException("Place");
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

        public async Task<ResponseBaseModel> PutPlaceAsync(Guid id, PlacesWriteDto input, CancellationToken cancellationToken)
        {
            var place = await PlaceQuery().FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false, cancellationToken);

            if (place is null)
            {
                throw new NotFoundException("Place");
            }

            var response = new ResponseBaseModel();
            await using var transaction = _context.Database.BeginTransaction();

            try
            {
                place.Address = input.Address;
                place.Longitude = input.Longitude;
                place.Lattitude = input.Lattitude;
                place.PlaceName = input.PlaceName;
                place.OwnerName = input.OwnerName;
                place.UpdatedAt = DateTime.UtcNow;

                _context.Update(place);
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

        public async Task<ResponseBaseModel> DeletePlaceAsync(Guid id, CancellationToken cancellationToken)
        {
            var place = await PlaceQuery().FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false, cancellationToken);

            if (place is null)
            {
                throw new NotFoundException("Place");
            }

            var response = new ResponseBaseModel();
            await using var transaction = _context.Database.BeginTransaction();

            try
            {
                place.IsDeleted = true;
                place.IsActive = false;
                place.UpdatedAt = DateTime.UtcNow;

                _context.Update(place);
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
