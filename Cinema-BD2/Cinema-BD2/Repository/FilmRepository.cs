using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private readonly CinemaContext _cinemaContext;

        public FilmRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }

        public async Task Create(Film film)
        {
            _cinemaContext.Films.Add(film);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Update(Film film)
        {
            _cinemaContext.Films.Update(film);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Film film)
        {
            _cinemaContext.Films.Remove(film);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Film>> GetAll()
        {
            return await _cinemaContext.Films
                .Include(x => x.Classification)
                .Include(x => x.Genres)
                .Include(x => x.Studios)
                .ToListAsync();
        }

        public async Task<Film?> GetById(int id)
        {
            return await _cinemaContext.Films
                .Include(x => x.Classification)
                .Include(x => x.Genres)
                .Include(x => x.Studios)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
