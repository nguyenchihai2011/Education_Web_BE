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
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(ApplicationDbContext context, IStudentRepository studentRepository, IMapper mapper)
        {
            this.context = context;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            return mapper.Map<IEnumerable<StudentDTO>>(await studentRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var student = await studentRepository.GetById(id);
                if (student != null)
                {
                    return Ok(mapper.Map<StudentDTO>(student));
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
        public async Task<IActionResult> Post([FromBody] StudentDTO studentDto)
        {
            try
            {
                var student = mapper.Map<StudentEntity>(studentDto);
                if (context.Admins.SingleOrDefault(a => a.UserId == student.UserId) == null
                    && context.Lectures.SingleOrDefault(a => a.UserId == student.UserId) == null
                    && context.Students.SingleOrDefault(a => a.UserId == student.UserId) == null)
                {
                    await studentRepository.Add(student);
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
        public async Task<IActionResult> Put(int id, [FromBody] StudentDTO student)
        {
            try
            {
                var updateStudent = await studentRepository.GetById(id);
                if (updateStudent != null)
                {
                    return Ok(mapper.Map<StudentDTO>(await studentRepository.Update(id, mapper.Map<StudentEntity>(student))));
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
                var student = await studentRepository.GetById(id);
                if (student != null)
                {
                    await studentRepository.Delete(student);
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
