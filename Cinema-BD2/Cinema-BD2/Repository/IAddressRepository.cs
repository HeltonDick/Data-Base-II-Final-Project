using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface IAddressRepository
    {
        Task Create(Address address);
        Task Update(Address address);
        Task Delete(Address address);
        Task<Address?> GetById(int id);
        Task<List<Address>> GetAll();
    }
}
