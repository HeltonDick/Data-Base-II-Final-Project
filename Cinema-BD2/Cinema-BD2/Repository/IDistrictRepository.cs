using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IDistrictRepository
    {
        public Task Create(District district);
        public Task Update(District district);
        public Task Delete(District district);

        public Task<District?> GetById(int id);
        public Task<List<District>> GetAll();
        public Task<List<District?>> GetByName(string name);
    }
}
