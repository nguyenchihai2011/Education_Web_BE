using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Enum;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                /*var student = await studentRepository.GetById(id);*/
                var student = context.Students.Include(s => s.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(od => od.Course).Where(s => s.Id == id).Select(c => new
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    AvatarUrl = c.AvatarUrl,
                    UserId = c.UserId,
                    Orders = c.Orders.Select(o => new
                    {
                        Id = o.Id,
                        Payment = o.Payment,
                        TotalPrice = o.TotalPrice,
                        CreateAt = o.CreateAt,
                        StudentId = o.StudentId,
                        OrderDetails = o.OrderDetails.Select(od => new
                        {
                            CourseId = od.CourseId,
                            Course = new { 
                               Id = od.Course.Id,
                               Name = od.Course.Name,
                               Title = od.Course.Title,
                               ImageUrl = od.Course.ImageUrl,
                               Description = od.Course.Description,
                               Lecture = od.Course.Lecture,
                               RatingAvg = od.Course.Ratings.Any() ? od.Course.Ratings.Average(r => r.Start) : 0,
                               TotalRatings = od.Course.Ratings.Count()
                            },
                            OrderId = od.OrderId,
                            Price = od.Price,
                        })

                    })
                }).FirstOrDefault();
                if (student != null)
                {
                    return Ok(student);
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
                var student = mapper.Map<Student>(studentDto);
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
                    return Ok(mapper.Map<StudentDTO>(await studentRepository.Update(id, mapper.Map<Student>(student))));
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
