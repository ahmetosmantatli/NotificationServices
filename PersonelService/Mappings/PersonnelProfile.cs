using AutoMapper;
using PersonelService.DTOs;
using PersonelService.Models;

namespace PersonelService.Mappings
{
    public class PersonnelProfile : Profile
    {
        public PersonnelProfile() {

            // DTO'dan Entity'ye mapping
            CreateMap<PersonnelDTO, Personnel>();

            // Entity'den DTO'ya mapping bu kullanımı not et 
            CreateMap<Personnel, PersonnelDTO>();


        }
    }
}
