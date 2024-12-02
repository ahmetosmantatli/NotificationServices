using PersonelService.DTOs;

namespace PersonelService.Services
{
    public interface IPersonnelService
    {
        Task AddPersonnelAsync(PersonnelDTO personnelDto);
        Task UpdatePersonnelAsync(int personnelId, PersonnelDTO personnelDto);
        Task DeletePersonnelAsync(int personnelId);
    }
}
