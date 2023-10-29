using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifycationController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly INotifycationRepository notifycationRepository;
        private readonly IMapper mapper;

        public NotifycationController(ApplicationDbContext context, INotifycationRepository notifycationRepository, IMapper mapper)
        {
            this.context = context;
            this.notifycationRepository = notifycationRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<NotifycationDTO>> Get()
        {
            return mapper.Map<IEnumerable<NotifycationDTO>>(await notifycationRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var notifycation = await notifycationRepository.GetById(id);
                if (notifycation != null)
                {
                    return Ok(mapper.Map<NotifycationDTO>(notifycation));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotifycationDTO notifycationDto)
        {
            try
            {
                var newNotifycation = await notifycationRepository.Add(mapper.Map<Notifycation>(notifycationDto));
                return Ok(mapper.Map<NotifycationDTO>(newNotifycation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] NotifycationDTO notifycationDto)
        {
            try
            {
                var updateNotifycation = await notifycationRepository.GetById(id);
                if (updateNotifycation != null)
                {
                    return Ok(mapper.Map<QuizDTO>(await notifycationRepository.Update(id, mapper.Map<Notifycation>(notifycationDto))));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var notifycation = await notifycationRepository.GetById(id);
                if (notifycation != null)
                {
                    await notifycationRepository.Delete(notifycation);
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
