using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;

namespace EducationAPI.Implement.Repositories
{
    public class RatingRepository : GenericRepository<RatingEntity>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
