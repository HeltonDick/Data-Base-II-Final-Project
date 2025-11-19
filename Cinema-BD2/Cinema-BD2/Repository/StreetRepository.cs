using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class StreetRepository : IStreetRepository
    {
        private readonly CinemaContext _cinemaContext;
        public StreetRepository (CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Street street)
        {
            await _cinemaContext.Streets.AddAsync(street);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Street street)
        {
            _cinemaContext.Streets.Remove(street);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Street>> GetAll()
        {
            return await _cinemaContext.Streets.ToListAsync();
        }

        public async Task<Street?> GetById(int id)
        {
            return await _cinemaContext.Streets
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Street?>> GetByName(string name)
        {
            return await _cinemaContext.Streets
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Street street)
        {
            _cinemaContext.Streets.Update(street);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
