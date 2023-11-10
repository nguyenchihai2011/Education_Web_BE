using EducationAPI.Data;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationAPI.Interfaces.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        List<Course> GetAll(string? search, double? from, double? to, string? sort, int page = 1, int size = 10);
    }
}
