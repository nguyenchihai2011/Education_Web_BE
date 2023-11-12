using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILessonRepository lessonRepository;
        private readonly IMapper mapper;

        public LessonController(ApplicationDbContext context, ILessonRepository lessonRepository, IMapper mapper)
        {
            this.context = context;
            this.lessonRepository = lessonRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = context.Lessons.Include(l => l.Section).ThenInclude(s => s.Course)
                .Select(l => new
                {
                    Id = l.Id,
                    Name = l.Name,
                    VideoUrl = l.VideoUrl,
                    Time = l.Time,
                    Index = l.Index,
                    IsReview = l.IsReview,
                    CreateAt = l.CreateAt,
                    UpdateAt = l.UpdateAt,
                    Section = new
                    {
                        Id = l.Section.Id,
                        Name = l.Section.Name,
                        Course = new
                        {
                            Id = l.Section.Course.Id,
                            Name = l.Section.Course.Name
                        }
                    }
                }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var lesson = await lessonRepository.GetById(id);
                if (lesson != null)
                {
                    return Ok(mapper.Map<LessonDTO>(lesson));
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
        public async Task<IActionResult> Post([FromBody] LessonDTO lessonDto)
        {
            try
            {
                var newLection = await lessonRepository.Add(mapper.Map<Lesson>(lessonDto));
                return Ok(mapper.Map<LessonDTO>(newLection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LessonDTO lessonDto)
        {
            try
            {
                var updateLesson = await lessonRepository.GetById(id);
                if (updateLesson != null)
                {
                    return Ok(mapper.Map<LessonDTO>(await lessonRepository.Update(id, mapper.Map<Lesson>(lessonDto))));
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
                var lesson = await lessonRepository.GetById(id);
                if (lesson != null)
                {
                    await lessonRepository.Delete(lesson);
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
