using Microsoft.EntityFrameworkCore;
using PersonelService.DbContexts;
using PersonelService.Models;
using System;

namespace PersonelService.Repository
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly PersonnelContext _context;

        public PersonnelRepository(PersonnelContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Personnel personnel)
        {
            await _context.Personnels.AddAsync(personnel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Personnel personnel)
        {
            _context.Personnels.Update(personnel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Personnel personnel)
        {
            _context.Personnels.Remove(personnel);
            await _context.SaveChangesAsync();
        }

        public async Task<Personnel> GetByIdAsync(int id)
        {
            return await _context.Personnels.FindAsync(id);
        }

        public async Task<IEnumerable<Personnel>> GetAllAsync()
        {
            return await _context.Personnels.ToListAsync();
        }
    }
}
