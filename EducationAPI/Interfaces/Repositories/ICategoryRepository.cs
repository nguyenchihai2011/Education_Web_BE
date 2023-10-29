using EducationAPI.Data;
using EducationAPI.Models;

namespace EducationAPI.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        List<CategoryDTO> GetAll(string? search);
    }
}
