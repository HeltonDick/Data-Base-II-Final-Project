using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Cinema_BD2.Repository
{
    public class GenderRepository : IGenderRepository
    {
        private readonly CinemaContext _cinemaContext;
        public GenderRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Gender gender)
        {
            await _cinemaContext.Genders.AddAsync(gender);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Gender gender)
        {
            _cinemaContext.Genders.Remove(gender);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Gender>> GetAll()
        {
            return await _cinemaContext.Genders.ToListAsync();
        }

        public async Task<Gender?> GetById(int id)
        {
            return await _cinemaContext.Genders
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Gender?>> GetByName(string name)
        {
            return await _cinemaContext.Genders
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Gender gender)
        {
            _cinemaContext.Genders.Update(gender);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
