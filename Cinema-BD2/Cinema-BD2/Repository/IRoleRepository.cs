using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IRoleRepository
    {
        public Task Create(Role role);
        public Task Update(Role role);
        public Task Delete(Role role);

        public Task<Role?> GetById(int id);
        public Task<List<Role>?> GetAll();
        public Task<List<Role?>> GetByName(string name);
    }
}
