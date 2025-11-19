using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface ISessionRepository
    {
        Task Create(Session Session);
        Task Update(Session Session);
        Task Delete(Session Session);

        public Task<Session?> GetById(int id);
        public Task<List<Session>> GetAll();
    }
}
