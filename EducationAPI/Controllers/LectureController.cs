using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILectureRepository lectureRepository;
        private readonly IMapper mapper;

        public LectureController(ApplicationDbContext context, ILectureRepository lectureRepository, IMapper mapper)
        {
            this.context = context;
            this.lectureRepository = lectureRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LectureDTO>> Get()
        {
            return mapper.Map<IEnumerable<LectureDTO>>(await lectureRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var lecture = await lectureRepository.GetById(id);
                if (lecture != null)
                {
                    return Ok(mapper.Map<LectureDTO>(lecture));
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
        public async Task<IActionResult> Post([FromBody] LectureDTO lectureDto)
        {
            try
            {
                var lecture = mapper.Map<Lecture>(lectureDto);
                if (context.Admins.SingleOrDefault(a => a.UserId == lecture.UserId) == null
                    && context.Lectures.SingleOrDefault(a => a.UserId == lecture.UserId) == null
                    && context.Students.SingleOrDefault(a => a.UserId == lecture.UserId) == null)
                {
                    await lectureRepository.Add(lecture);
                    return Ok(new ApiResponse { Success = true, Message = "Created success" });
                    
                }
                return BadRequest(
                     new ApiResponse { Success = false, Message = "UserId is exist" }
                 );

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LectureDTO lecture)
        {
            try
            {
                var updateLecture = await lectureRepository.GetById(id);
                if (updateLecture != null)
                {
                    return Ok(mapper.Map<LectureDTO>(await lectureRepository.Update(id, mapper.Map<Lecture>(lecture))));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lecture = await lectureRepository.GetById(id);
                if (lecture != null)
                {
                    await lectureRepository.Delete(lecture);
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
