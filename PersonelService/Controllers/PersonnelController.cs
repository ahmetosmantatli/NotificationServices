using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.DTOs;
using NotificationService.Services;
using PersonelService.DTOs;
using PersonelService.Models;
using PersonelService.Repository;
using PersonelService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonelService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnelController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonnel([FromBody] PersonnelDTO personnelDto)
        {
            if (personnelDto == null || string.IsNullOrWhiteSpace(personnelDto.Email))
            {
                return BadRequest("Personnel data or email is missing.");
            }

            await _personnelService.AddPersonnelAsync(personnelDto);
            return Ok(personnelDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonnel(int id, [FromBody] PersonnelDTO personnelDto)
        {
            await _personnelService.UpdatePersonnelAsync(id, personnelDto);
            return Ok(personnelDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonnel(int id)
        {
            await _personnelService.DeletePersonnelAsync(id);
            return Ok();
        }
    }
}
