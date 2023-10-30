using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ApplicationDbContext context, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.context = context;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

      /*  [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            return mapper.Map<IEnumerable<CategoryDTO>>(await categoryRepository.GetAllAsync());
        }*/

        [HttpGet]
        public IActionResult GetAll(string? search)
        {
            try
            {
                var result = categoryRepository.GetAll(search);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                /*var category = await categoryRepository.GetById(id);*/
                var category = context.Categories.Include(c => c.Courses)
                    .ThenInclude(c => c.Lecture)
                    .Where(c => c.Id == id)
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Courses = c.Courses.Select(cor => new Course
                        {
                            Id = cor.Id,
                            Name = cor.Name,
                            Title = cor.Title,
                            ImageUrl = cor.ImageUrl,
                            Price = cor.Price,
                            Lecture = cor.Lecture,
                        }).ToList(),
                    }).FirstOrDefault();
                if (category != null)
                {
                    return Ok(category);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDTO category)
        {
            try
            {
                var newCategory = await categoryRepository.Add(mapper.Map<Category>(category));
                return Ok(mapper.Map<CategoryDTO>(newCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO category)
        {
            try
            {
                var updateCategory = await categoryRepository.GetById(id);
                if (updateCategory != null)
                {
                    return Ok(mapper.Map<CategoryDTO>(await categoryRepository.Update(id, mapper.Map<Category>(category))));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await categoryRepository.GetById(id);
                if (category != null)
                {
                    await categoryRepository.Delete(category);
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/category/bulkdelete")]
        public async Task<IActionResult> Delete([FromBody] int[] ids)
        {
            try
            {
                foreach(var id in ids)
                {
                    var category = await categoryRepository.GetById(id);
                    await categoryRepository.Delete(category);
                }
                return NoContent();         
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
