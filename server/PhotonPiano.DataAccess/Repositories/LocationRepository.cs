using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly PhotonPianoContext _context;
        public LocationRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Location>> GetQueriedLocationAsync(QueryLocationDto queryLocationDto)
        {
            var locationQuery = _context.Locations
                .AsQueryable();

            if (queryLocationDto.Id.HasValue)
            {
                locationQuery = locationQuery.Where(l => l.Id == queryLocationDto.Id.Value);
            }
            if (queryLocationDto.Name is not null)
            {
                locationQuery = locationQuery.Where(l => l.Name.Contains(queryLocationDto.Name));
            }
            if (queryLocationDto.Status is not null)
            {
                locationQuery = locationQuery.Where(l => l.Status == queryLocationDto.Status);
            }
            if (queryLocationDto.CapacityFrom.HasValue)
            {
                locationQuery = locationQuery.Where(l => l.Capacity <= queryLocationDto.CapacityFrom.Value);
            }
            if (queryLocationDto.CapacityTo.HasValue)
            {
                locationQuery = locationQuery.Where(l => l.Capacity >= queryLocationDto.CapacityTo.Value);
            }

            var result = locationQuery.ToListAsync();
            return result;
        }
    }
}