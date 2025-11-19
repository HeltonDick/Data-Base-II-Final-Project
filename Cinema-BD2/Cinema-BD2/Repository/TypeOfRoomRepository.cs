using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{

    public class TypeOfRoomRepository : ITypeOfRoomRepository
    {
        private readonly CinemaContext _cinemaContext;
        public TypeOfRoomRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(TypeOfRoom typeOfRoom)
        {
            await _cinemaContext.TypeOfRooms.AddAsync(typeOfRoom);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(TypeOfRoom typeOfRoom)
        {
            _cinemaContext.TypeOfRooms.Remove(typeOfRoom);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<TypeOfRoom>> GetAll()
        {
            return await _cinemaContext.TypeOfRooms.ToListAsync();
        }

        public async Task<TypeOfRoom?> GetById(int id)
        {
            return await _cinemaContext.TypeOfRooms
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TypeOfRoom?>> GetByName(string name)
        {
            return await _cinemaContext.TypeOfRooms
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(TypeOfRoom typeOfRoom)
        {
            _cinemaContext.TypeOfRooms.Update(typeOfRoom);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
