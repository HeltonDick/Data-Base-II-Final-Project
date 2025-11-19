using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class DimensionRepository : IDimensionRepository
    {
        private readonly CinemaContext _cinemaContext;
        public DimensionRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }

        public async Task Create(Dimension dimension)
        {
            await _cinemaContext.Dimensions.AddAsync(dimension);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Dimension dimension)
        {
            _cinemaContext.Dimensions.Remove(dimension);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Dimension>> GetAll()
        {
            return await _cinemaContext.Dimensions.ToListAsync();
        }

        public async Task<Dimension?> GetById(int id)
        {
            return await _cinemaContext.Dimensions
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Dimension?>> GetByName(string name)
        {
            return await _cinemaContext.Dimensions
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Dimension dimension)
        {
            _cinemaContext.Dimensions.Update(dimension);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
