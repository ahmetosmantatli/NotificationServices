using AutoMapper;
using NotificationService.DTOs;
using NotificationService.Services;
using PersonelService.DTOs;
using PersonelService.Models;
using PersonelService.Repository;

namespace PersonelService.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _personnelRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public PersonnelService(IPersonnelRepository personnelRepository, INotificationService notificationService, IMapper mapper)
        {
            _personnelRepository = personnelRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task AddPersonnelAsync(PersonnelDTO personnelDto)
        {
            var personnel = _mapper.Map<Personnel>(personnelDto);
            await _personnelRepository.AddAsync(personnel);

            var notificationDto = new NotificationDto
            {
                RecipientEmail = personnel.Email,
                Subject = "Personnel Added",
                Body = "A new personnel has been added."
            };
           // var mail = personnel.Email;
            await _notificationService.SendEmail(notificationDto);
        }

        public async Task UpdatePersonnelAsync(int personnelId, PersonnelDTO personnelDto)
        {
            var personnel = await _personnelRepository.GetByIdAsync(personnelId);
            if (personnel != null)
            {
                _mapper.Map(personnelDto, personnel);
                await _personnelRepository.UpdateAsync(personnel);

                var notificationDto = new NotificationDto
                {
                    RecipientEmail = personnel.Email,
                    Subject = "Personnel Updated",
                    Body = "The personnel has been updated."
                };

                await _notificationService.SendEmail(notificationDto);
            }
        }

        public async Task DeletePersonnelAsync(int personnelId)
        {
            var personnel = await _personnelRepository.GetByIdAsync(personnelId);
            if (personnel != null)
            {
                await _personnelRepository.DeleteAsync(personnel);

                var notificationDto = new NotificationDto
                {
                    RecipientEmail = personnel.Email,
                    Subject = "Personnel Deleted",
                    Body = "The personnel has been deleted."
                };

                await _notificationService.SendEmail(notificationDto);
            }
        }
    }
}
