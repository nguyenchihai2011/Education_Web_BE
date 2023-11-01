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

        /*[HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {
            return mapper.Map<IEnumerable<CourseDTO>>(await courseRepository.GetAllAsync());
        }*/

        [HttpGet]
        public IActionResult GetAll(string? search, string? level, int? categoryId, double? rating, double? from, double? to, string? sort, int page = 1, int size = 10)
        {
            try
            {
                var allCourse = context.Courses.Include(c => c.Lecture).AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    allCourse = allCourse.Where(course => course.Name.Contains(search));
                }
                if (categoryId.HasValue)
                {
                    allCourse = allCourse.Where(course => course.CategoryId == categoryId);
                }
                if (from.HasValue)
                {
                    allCourse = allCourse.Where(course => course.Price >= from);
                }
                if (to.HasValue)
                {
                    allCourse = allCourse.Where(course => course.Price <= to);
                }

                if (rating.HasValue)
                {
                    allCourse = allCourse.Where(course => (course.Ratings.Any() ? course.Ratings.Average(r => r.Start) : 0) >= rating);
                }
                if (!string.IsNullOrEmpty(level))
                {
                    allCourse = allCourse.Where(course => course.Level == level);
                }

                if (!string.IsNullOrEmpty(sort))
                {
                    switch (sort)
                    {
                        case "price":
                            allCourse = allCourse.OrderBy(c => c.Price);
                            break;
                        case "-price":
                            allCourse = allCourse.OrderByDescending(c => c.Price);
                            break;

                    }
                }
                var totalPage = (int)Math.Ceiling(allCourse.Count() / (double)size);
                allCourse = allCourse.Skip((page - 1) * size).Take(size);

                var result = allCourse.Select(course => new
                {
                    Id = course.Id,
                    Name = course.Name,
                    Title = course.Title,
                    ImageUrl = course.ImageUrl,
                    Description = course.Description,
                    Price = course.Price,
                    Level = course.Level,
                    Language = course.Language,
                    LectureId = course.LectureId,
                    Lecture = course.Lecture,
                    CategoryId = course.CategoryId,
                    PromotionId = course.PromotionId,
                    RatingAvg = course.Ratings.Any() ? course.Ratings.Average(r => r.Start) : 0,
                    TotalRatings = course.Ratings.Count()
                });

                return Ok(new
                {
                    result = result,
                    totalPage = totalPage
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        double CalculateAverageRating(Course course)
        {
            // Logic to calculate the average rating for a course
            // Replace this with the actual logic specific to your implementation
            // Example logic:
            return course.Ratings.Any() ? course.Ratings.Average(r => r.Start) : 0;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                /*var course = await courseRepository.GetById(id);*/
                var course = context.Courses.Include(c => c.Lecture).Include(c => c.Sections)
                    .ThenInclude(s => s.Lessons)
                    .Where(c => c.Id == id)
                    .Select(c => new CourseDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Title = c.Title,
                        ImageUrl = c.ImageUrl,
                        Description = c.Description,
                        Price = c.Price,
                        Level = c.Level,
                        Language = c.Language,
                        LectureId = c.LectureId,
                        Lecture = c.Lecture,
                        Sections = c.Sections.Select(s => new Section {
                            Id = s.Id,
                            Name = s.Name,
                            CourseId = s.CourseId,
                            Index = s.Index,
                            Lessons = s.Lessons.Select(l => new Lesson
                            {
                                Id = l.Id,
                                Name = l.Name,
                                VideoUrl = l.VideoUrl,
                                Index = l.Index,
                                Time = l.Time,
                                SectionId = l.SectionId,
                                StudentLessons = l.StudentLessons
                                .Where(sl => sl.LessonId == l.Id)
                                .Select(c => new StudentLesson
                                {
                                    IsLock = c.IsLock
                                }).ToList(),
                            }).OrderBy(l => l.Index).ToList(),
                        }).OrderBy(s => s.Index).ToList(),
                        CategoryId = c.CategoryId,
                        PromotionId = c.PromotionId
                    }).FirstOrDefault();
            
                if (course != null)
                {
                    return Ok(course);
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
                var newCourse = await courseRepository.Add(mapper.Map<Course>(courseDto));
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
                    return Ok(mapper.Map<CourseDTO>(await courseRepository.Update(id, mapper.Map<Course>(courseDto))));
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

        [HttpDelete("/api/course/bulkdelete")]
        public async Task<IActionResult> Delete([FromBody] int[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var course = await courseRepository.GetById(id);
                    await courseRepository.Delete(course);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
