using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IStreetRepository
    {
        public Task Create(Street street);
        public Task Update(Street street);
        public Task Delete(Street street);

        public Task<Street?> GetById(int id);
        public Task<List<Street>> GetAll();
        public Task<List<Street?>> GetByName(string name);
    }
}
