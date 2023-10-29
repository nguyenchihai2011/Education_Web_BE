using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Implement.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<CategoryDTO> GetAll(string? search)
        {
            var allCategory = context.Categories.AsQueryable();

            if(!string.IsNullOrEmpty(search))
            {
                allCategory = allCategory.Where(category => category.Name.Contains(search));
            }
            var result = allCategory.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            });
            return result.ToList();
            
        }
    }
}
