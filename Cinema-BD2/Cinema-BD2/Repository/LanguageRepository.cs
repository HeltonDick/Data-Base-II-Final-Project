using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly CinemaContext _cinemaContext;
        public LanguageRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Language language)
        {
            await _cinemaContext.Languages.AddAsync(language);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Language language)
        {
            _cinemaContext.Languages.Remove(language);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Language>> GetAll()
        {
            return await _cinemaContext.Languages.ToListAsync();
        }

        public async Task<Language?> GetById(int id)
        {
            return await _cinemaContext.Languages
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Language?>> GetByName(string name)
        {
            return await _cinemaContext.Languages
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Language language)
        {
            _cinemaContext.Languages.Update(language);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
