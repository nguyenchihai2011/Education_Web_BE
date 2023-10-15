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
    public class SectionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ISectionRepository sectionRepository;
        private readonly IMapper mapper;

        public SectionController(ApplicationDbContext context, ISectionRepository sectionRepository, IMapper mapper)
        {
            this.context = context;
            this.sectionRepository = sectionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SectionDTO>> Get()
        {
            return mapper.Map<IEnumerable<SectionDTO>>(await sectionRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var section = await sectionRepository.GetById(id);
                if (section != null)
                {
                    return Ok(mapper.Map<SectionDTO>(section));
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
        public async Task<IActionResult> Post([FromBody] SectionDTO sectionDto)
        {
            try
            {
                var newSection = await sectionRepository.Add(mapper.Map<SectionEntity>(sectionDto));
                return Ok(mapper.Map<SectionDTO>(newSection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SectionDTO sectionDto)
        {
            try
            {
                var updateSection = await sectionRepository.GetById(id);
                if (updateSection != null)
                {
                    return Ok(mapper.Map<SectionDTO>(await sectionRepository.Update(id, mapper.Map<SectionEntity>(sectionDto))));
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
                var section = await sectionRepository.GetById(id);
                if (section != null)
                {
                    await sectionRepository.Delete(section);
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
