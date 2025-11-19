using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IStudioRepository
    {
        public Task Create(Studio studio);
        public Task Update(Studio studio);
        public Task Delete(Studio studio);

        public Task<Studio?> GetById(int id);
        public Task<List<Studio>> GetAll();
        public Task<List<Studio?>> GetByName(string name);

        Task<List<Studio>> GetSelected(int[] ids);
    }
}
