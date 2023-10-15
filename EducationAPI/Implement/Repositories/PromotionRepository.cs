using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;

namespace EducationAPI.Implement.Repositories
{
    public class PromotionRepository : GenericRepository<PromotionEntity>, IPromotionRepository
    {
        public PromotionRepository (ApplicationDbContext context) : base(context)
        {
        }
    }
}
