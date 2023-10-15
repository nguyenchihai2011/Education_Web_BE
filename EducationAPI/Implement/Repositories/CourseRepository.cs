using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;

namespace EducationAPI.Implement.Repositories
{
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
