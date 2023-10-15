using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
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
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public CourseController(ApplicationDbContext context, ICourseRepository courseRepository, IMapper mapper)
        {
            this.context = context;
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {
            return mapper.Map<IEnumerable<CourseDTO>>(await courseRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var course = await courseRepository.GetById(id);
                if (course != null)
                {
                    return Ok(mapper.Map<CourseDTO>(course));
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
        public async Task<IActionResult> Post([FromBody] CourseDTO courseDto)
        {
            try
            {
                var newCourse = await courseRepository.Add(mapper.Map<CourseEntity>(courseDto));
                return Ok(mapper.Map<CourseDTO>(newCourse));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CourseDTO courseDto)
        {
            try
            {
                var updateCourse = await courseRepository.GetById(id);
                if (updateCourse != null)
                {
                    return Ok(mapper.Map<CourseDTO>(await courseRepository.Update(id, mapper.Map<CourseEntity>(courseDto))));
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
                var course = await courseRepository.GetById(id);
                if (course != null)
                {
                    await courseRepository.Delete(course);
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
