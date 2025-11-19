using Cinema_BD2.Models;
using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Repository
{
    public interface IGenderRepository
    {
        public Task Create(Gender gender);
        public Task Update(Gender gender);
        public Task Delete(Gender gender);

        public Task<Gender?> GetById(int id);
        public Task<List<Gender>> GetAll();
        public Task<List<Gender?>> GetByName(string name);
    }
}
