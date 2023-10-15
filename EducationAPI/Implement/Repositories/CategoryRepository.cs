using EducationAPI.Context;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;

namespace EducationAPI.Implement.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
