using Cinema_BD2.Models;

namespace Cinema_BD2.Repository
{
    public interface ILanguageRepository
    {
        public Task Create(Language language);
        public Task Update(Language language);
        public Task Delete(Language language);

        public Task<Language?> GetById(int id);
        public Task<List<Language>> GetAll();
        public Task<List<Language?>> GetByName(string name);
    }
}
