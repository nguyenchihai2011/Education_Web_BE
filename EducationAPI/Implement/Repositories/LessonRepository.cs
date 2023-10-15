using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;

namespace EducationAPI.Implement.Repositories
{
    public class LessonRepository : GenericRepository<LessonEntity>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
