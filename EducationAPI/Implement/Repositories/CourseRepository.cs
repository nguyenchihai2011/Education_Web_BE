using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Implement.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext context;

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<Course> GetAll(string? search, double? from, double? to, string? sort, int page = 1, int size = 10)
        {
            var allCourse = context.Courses.Include(c => c.Lecture).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                allCourse = allCourse.Include(c => c.Lecture).Where(course => course.Name.Contains(search));
            }
            if (from.HasValue)
            {
                allCourse = allCourse.Where(course => course.Price >= from);
            }
            if (to.HasValue)
            {
                allCourse = allCourse.Where(course => course.Price <= to);
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

            allCourse = allCourse.Skip((page - 1) * size).Take(size);

            var result = allCourse.Select(course => new Course
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
                PromotionId = course.PromotionId
            });

            return result.ToList();

            /*var result = PaginatedList<Course>.Create(allCourse, page, size);

            return result.Select(course => new CourseDTO
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
                PromotionId = course.PromotionId
            }).ToList();*/

        }
    }
}
