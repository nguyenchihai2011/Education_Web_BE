using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;

namespace EducationAPI.Implement.Repositories
{
    public class LectureRepository : GenericRepository<LectureEntity>, ILectureRepository
    {
        public LectureRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
