using Cinema_BD2.Data;
using Cinema_BD2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_BD2.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CinemaContext _context;

        public AddressRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAll()
        {
            return await _context.Addresses
                .Include(a => a.Street)
                .Include(a => a.District)
                .ToListAsync();
        }

        public async Task<Address?> GetById(int id)
        {
            return await _context.Addresses
                .Include(a => a.Street)
                .Include(a => a.District)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Create(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}
