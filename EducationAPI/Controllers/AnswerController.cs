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
    public class AnswerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAnswerRepository answerRepository;
        private readonly IMapper mapper;

        public AnswerController(ApplicationDbContext context, IAnswerRepository answerRepository, IMapper mapper)
        {
            this.context = context;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AnswerDTO>> Get()
        {
            return mapper.Map<IEnumerable<AnswerDTO>>(await answerRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var answer = await answerRepository.GetById(id);
                if (answer != null)
                {
                    return Ok(mapper.Map<AnswerDTO>(answer));
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
        public async Task<IActionResult> Post([FromBody] AnswerDTO answerDto)
        {
            try
            {
                var newAnswer = await answerRepository.Add(mapper.Map<Answer>(answerDto));
                return Ok(mapper.Map<AnswerDTO>(newAnswer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AnswerDTO answerDto)
        {
            try
            {
                var updateAnswer = await answerRepository.GetById(id);
                if (updateAnswer != null)
                {
                    return Ok(mapper.Map<AnswerDTO>(await answerRepository.Update(id, mapper.Map<Answer>(answerDto))));
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
                var answer = await answerRepository.GetById(id);
                if (answer != null)
                {
                    await answerRepository.Delete(answer);
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
