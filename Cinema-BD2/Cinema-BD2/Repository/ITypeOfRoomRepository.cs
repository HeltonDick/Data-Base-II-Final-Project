using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface ITypeOfRoomRepository
    {
        public Task Create(TypeOfRoom typeOfRoom);
        public Task Update(TypeOfRoom typeOfRoom);
        public Task Delete(TypeOfRoom typeOfRoom);

        public Task<TypeOfRoom?> GetById(int id);
        public Task<List<TypeOfRoom>> GetAll();
        public Task<List<TypeOfRoom?>> GetByName(string name);
    }
}
