using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLessonController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public StudentLessonController(ApplicationDbContext context, ICourseRepository courseRepository, IMapper mapper)
        {
            this.context = context;
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }
        // GET: api/<StudentLessonController>
        [HttpGet]
        public async Task<IActionResult> Get(int courseId, int studentId)
        {
            var lessons = context.StudentLessons
                .Include(sl => sl.Lesson)
                .Where(sl => sl.StudentId == studentId && sl.Lesson.Section.CourseId == courseId)
                .Select(sl => new StudentLesson
                {
                    StudentId = sl.StudentId,
                    LessonId = sl.LessonId,
                    IsLock = sl.IsLock,
                    Lesson = sl.Lesson
                })
                .ToList();
            return Ok(lessons);
        }

        // GET api/<StudentLessonController>/5
        [HttpGet("/api/getProgress")]
        public async Task<IActionResult> GetProgress(int courseId, int studentId)
        {
            var totalLessons = context.StudentLessons.Count(sl => sl.StudentId == studentId && sl.Lesson.Section.CourseId == courseId);
            if (totalLessons == 0)
            {
                return NotFound();
            }

            var unlockedLessons = context.StudentLessons.Count(sl => sl.StudentId == studentId && sl.Lesson.Section.CourseId == courseId && !sl.IsLock);
            var lockedLessons = totalLessons - unlockedLessons;
            /*double percentage = (double)unlockedLessons / totalLessons * 100;*/
            return Ok(new int[]
            {
                lockedLessons,
                unlockedLessons
            });
        }

        // POST api/<StudentLessonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentLessonController>/5
        [HttpPut]
        public async Task<IActionResult> UnlockLesson(int lessonId, int studentId, int courseId)
        {
            var lesson = context.StudentLessons
                .SingleOrDefault(sl => sl.StudentId == studentId && sl.LessonId == lessonId);
            if (lesson != null)
            {
                lesson.IsLock = !lesson.IsLock;
            }
            context.StudentLessons.Update(lesson);
            context.SaveChanges();

            var totalLessons = context.StudentLessons.Count(sl => sl.StudentId == studentId && sl.Lesson.Section.CourseId == courseId);
            if (totalLessons == 0)
            {
                return NotFound();
            }

            var unlockedLessons = context.StudentLessons.Count(sl => sl.StudentId == studentId && sl.Lesson.Section.CourseId == courseId && !sl.IsLock);
            var lockedLessons = totalLessons - unlockedLessons;
            /*double percentage = (double)unlockedLessons / totalLessons * 100;*/
            return Ok(new int[]
            {
                lockedLessons,
                unlockedLessons
            });

        }

        // DELETE api/<StudentLessonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
