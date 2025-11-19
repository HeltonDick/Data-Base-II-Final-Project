using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IDimensionRepository
    {
        Task Create(Dimension dimension);
        Task Update(Dimension dimension);
        Task Delete(Dimension dimension);

        public Task<Dimension?> GetById(int id);
        public Task<List<Dimension>> GetAll();
        public Task<List<Dimension>> GetByName(string Name);
    }
}
