using PersonelService.Models;

namespace PersonelService.Repository
{
    public interface IPersonnelRepository
    {
        Task AddAsync(Personnel personnel);
        Task UpdateAsync(Personnel personnel);
        Task DeleteAsync(Personnel personnel);
        Task<Personnel> GetByIdAsync(int id);
    }
}
