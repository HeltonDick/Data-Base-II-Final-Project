using Cinema_BD2.Models;
using System.Data.SqlTypes;

namespace Cinema_BD2.Repository
{
    public interface IRoomRepository
    {
        Task Create(Room room);
        Task Update(Room room);
        Task Delete(Room room);

        Task<Room?> GetById(int id);
        Task<List<Room>> GetAll();
        Task <List<Room>> GetByName(string name);
    }
}
