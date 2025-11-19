using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cinema_BD2.Repository
{
    public class StudioRepository : IStudioRepository
    {
        private readonly CinemaContext _cinemaContext;
        public StudioRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Studio studio)
        {
            await _cinemaContext.Studios.AddAsync(studio);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Studio studio)
        {
            _cinemaContext.Studios.Remove(studio);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Studio>> GetAll()
        {
            return await _cinemaContext.Studios.ToListAsync();
        }

        public async Task<Studio?> GetById(int id)
        {
            return await _cinemaContext.Studios
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Studio?>> GetByName(string name)
        {
            return await _cinemaContext.Studios
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Studio studio)
        {
            _cinemaContext.Studios.Update(studio);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Studio>> GetSelected(int[] ids)
        {
            return await _cinemaContext.Studios
                .Where(g => ids.Contains(g.Id))
                .ToListAsync();
        }
    }
}
