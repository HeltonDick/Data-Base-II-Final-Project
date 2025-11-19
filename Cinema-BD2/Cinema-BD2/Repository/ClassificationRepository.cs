using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly CinemaContext _cinemaContext;
        public ClassificationRepository(CinemaContext context)
        {
            _cinemaContext = context;
        }

        public async Task Create(Classification classification)
        {
            await _cinemaContext.Classifications.AddAsync(classification);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Classification classification)
        {
            _cinemaContext.Classifications.Remove(classification);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Classification>> GetAll()
        {
            return await _cinemaContext.Classifications.ToListAsync();
        }

        public async Task<Classification?> GetById(int id)
        {
            return await _cinemaContext.Classifications
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<List<Classification?>> GetByMinAge(int age)
        {
            return _cinemaContext.Classifications
                .Where(c => c.MinAge == age)
                .ToListAsync();
        }

        public async Task Update(Classification classification)
        {
            _cinemaContext.Classifications.Update(classification);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
