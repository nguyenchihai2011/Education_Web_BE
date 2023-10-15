using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;

namespace EducationAPI.Implement.Repositories
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
