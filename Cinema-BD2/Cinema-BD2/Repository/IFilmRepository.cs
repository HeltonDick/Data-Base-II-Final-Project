using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IFilmRepository
    {
        Task Create(Film film);
        Task Update(Film film);
        Task Delete(Film film);

        Task<Film?> GetById(int id);
        Task<List<Film>> GetAll();
    }
}
