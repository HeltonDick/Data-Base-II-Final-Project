using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);
        Task Update(Ticket ticket);
        Task Delete(Ticket ticket);

        Task<Ticket?> GetById(int id);
        Task<List<Ticket>> GetAll();
    }
}
