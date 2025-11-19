using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IPerosonRepository
    {
        Task Create(Person person);
        Task Update(Person person);
        Task Delete(Person person);

        Task<Person?> GetById(int id);
        Task<List<Person>> GetAll();
        Task<List<Person>> GetByName(string name);
        Task<bool> ExistsCpf(string cpf);
    }
}
