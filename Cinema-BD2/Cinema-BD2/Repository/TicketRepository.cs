using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaContext _cinemaContext;
        public TicketRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Ticket ticket)
        {
            _cinemaContext.Tickets.Add(ticket);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Ticket ticket)
        {
            _cinemaContext.Tickets.Remove(ticket);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _cinemaContext.Tickets
                .Include(r => r.Person)
                .Include(r => r.Session)
                .ToListAsync();
        }

        public async Task Update(Ticket ticket)
        {
            _cinemaContext.Tickets.Update(ticket);
            await _cinemaContext.SaveChangesAsync();
        }

        async Task<Ticket?> ITicketRepository.GetById(int id)
        {
            return await _cinemaContext.Tickets
                .Include(r => r.Person)
                .Include(r => r.Session)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
