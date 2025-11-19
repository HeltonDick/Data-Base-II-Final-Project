using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly CinemaContext _cinemaContext;
        public SessionRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Session Session)
        {
            _cinemaContext.Sessions.Add(Session);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Session Session)
        {
            _cinemaContext.Sessions.Remove(Session);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Session>> GetAll()
        {
            return await _cinemaContext.Sessions
                .Include(s => s.Language)
                .Include(s => s.Room)
                .Include(s => s.Dimension)
                .Include(s => s.Film)
                .ToListAsync();
        }

        public async Task<Session?> GetById(int id)
        {
            return await _cinemaContext.Sessions
                .Include(r => r.Language)
                .Include(s => s.Room)
                .Include(s => s.Dimension)
                .Include(s => s.Film)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Update(Session Session)
        {
            _cinemaContext.Sessions.Update(Session);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
