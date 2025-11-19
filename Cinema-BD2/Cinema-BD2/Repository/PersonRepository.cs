using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class PersonRepository : IPerosonRepository
    {
        private readonly CinemaContext _context;
        public PersonRepository(CinemaContext context)
        {
            _context = context;
        }
        public async Task Create(Person person)
        {
            person.FormatContact();
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Person person)
        {
            person.FormatContact();
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person?> GetById(int id)
        {
            return await _context.People
                .Include(p => p.Gender)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Street)
                .Include(p => p.Address)
                    .ThenInclude(a => a.District)
                .Include(p => p.Roles)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Person>> GetAll()
        {   
            return await _context.People
                .Include(p => p.Gender)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Street)
                .Include(p => p.Address)
                    .ThenInclude(a => a.District)
                .Include(p => p.Roles)
                .ToListAsync();
        }

        public async Task<List<Person>> GetByName(string name)
        {
            return await _context.People
              .Include(p => p.Gender)
              .Include(p => p.Roles)
              .Where(p => p.Name.ToLower().Contains(name.ToLower()))
              .ToListAsync();
        }

        public async Task<bool> ExistsCpf(string cpf)
        {
            return await _context.People.AnyAsync(p => p.Cpf == cpf);
        }
    }
}
