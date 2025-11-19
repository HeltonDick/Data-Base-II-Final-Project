using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IGenreRepository
    {
        public Task Create(Genre genre);
        public Task Update(Genre genre);
        public Task Delete(Genre genre);

        public Task<Genre?> GetById(int id);
        public Task<List<Genre>> GetAll();
        public Task<List<Genre?>> GetByName(string name);

        Task<List<Genre>> GetSelected(int[] ids);
    }
}
