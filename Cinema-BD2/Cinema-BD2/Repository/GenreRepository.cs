using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CinemaContext _cinemaContext;
        public GenreRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Genre genre)
        {
            await _cinemaContext.Genres.AddAsync(genre);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Genre genre)
        {
            _cinemaContext.Genres.Remove(genre);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _cinemaContext.Genres.ToListAsync();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _cinemaContext.Genres
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Genre?>> GetByName(string name)
        {
            return await _cinemaContext.Genres
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Genre genre)
        {
            _cinemaContext.Genres.Update(genre);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Genre>> GetSelected(int[] ids)
        {
            return await _cinemaContext.Genres
                .Where(g => ids.Contains(g.Id))
                .ToListAsync();
        }
    }
}
