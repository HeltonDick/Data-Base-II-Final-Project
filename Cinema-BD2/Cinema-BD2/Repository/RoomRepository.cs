using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Cinema_BD2.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CinemaContext _cinemaContext;
        public RoomRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Room room)
        {
            _cinemaContext.Rooms.Add(room);
            await _cinemaContext.SaveChangesAsync();
        }
        public async Task Delete(Room room)
        {
            _cinemaContext.Rooms.Remove(room);
            await _cinemaContext.SaveChangesAsync();
        }
        public async Task<List<Room>> GetAll()
        {
            return await _cinemaContext.Rooms
                .Include(r => r.TypeOfRoom)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }
        public async Task<Room?> GetById(int id)
        {
            return await _cinemaContext.Rooms
                .Include(r => r.TypeOfRoom)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Room>> GetByName(string name)
        {
           return await _cinemaContext.Rooms
                .Where(r => r.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();

        }

        public async Task Update(Room room)
        {
            _cinemaContext.Rooms.Update(room);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
