using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CinemaContext _cinemaContext;
        public RoleRepository(CinemaContext cinemaContext)
        {
            _cinemaContext = cinemaContext;
        }
        public async Task Create(Role role)
        {
            await _cinemaContext.Roles.AddAsync(role);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task Delete(Role role)
        {
            _cinemaContext.Roles.Remove(role);
            await _cinemaContext.SaveChangesAsync();
        }

        public async Task<List<Role>> GetAll()
        {
            return await _cinemaContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetById(int id)
        {
            return await _cinemaContext.Roles
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Role?>> GetByName(string name)
        {
            return await _cinemaContext.Roles
                .Where(w => w.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Role role)
        {
            _cinemaContext.Roles.Update(role);
            await _cinemaContext.SaveChangesAsync();
        }
    }
}
