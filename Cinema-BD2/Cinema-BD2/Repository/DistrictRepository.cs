using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly CinemaContext _cinemaContext;
        public DistrictRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }

        public async Task Create(District district)
        {
            await _cinemaContext.Districts.AddAsync(district);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(District district)
        {
            _cinemaContext.Districts.Remove(district);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<District>> GetAll()
        {
            return await _cinemaContext.Districts.ToListAsync();
        }

        public async Task<District?> GetById(int id)
        {
            return await _cinemaContext.Districts
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<District?>> GetByName(string name)
        {
            return await _cinemaContext.Districts
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();

        }

        public async Task Update(District district)
        {
            _cinemaContext.Districts.Update(district);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
