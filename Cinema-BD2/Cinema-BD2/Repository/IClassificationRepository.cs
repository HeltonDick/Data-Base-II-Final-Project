using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IClassificationRepository
    {
        public Task Create(Classification classification);
        public Task Update(Classification classification);
        public Task Delete(Classification classification);

        public Task<Classification?> GetById(int id);
        public Task<List<Classification>> GetAll();
        public Task<List<Classification?>> GetByMinAge(int age);
    }
}
