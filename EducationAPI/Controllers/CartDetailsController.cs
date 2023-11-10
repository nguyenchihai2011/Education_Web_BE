using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICartDetailsRepository cartDetailsRepository;
        private readonly IMapper mapper;

        public CartDetailsController(ApplicationDbContext context, ICartDetailsRepository cartDetailsRepository, IMapper mapper)
        {
            this.context = context;
            this.cartDetailsRepository = cartDetailsRepository;
            this.mapper = mapper;
        }

       /* [HttpGet]
        public async Task<IEnumerable<CartDetails>> Get()
        {
            return mapper.Map<IEnumerable<CartDTO>>(await cartRepository.GetAllAsync());
        }*/

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cart = context.CartDetails.Where(c => c.CartId == id);
                if (cart != null)
                {
                    return Ok(cart.Select(course => new
                    {
                        Id = course.Course.Id,
                        Name = course.Course.Name,
                        Title = course.Course.Title,
                        ImageUrl = course.Course.ImageUrl,
                        Description = course.Course.Description,
                        Price = course.Course.Price,
                        Level = course.Course.Level,
                        Language = course.Course.Language,
                        LectureId = course.Course.LectureId,
                        Lecture = course.Course.Lecture,
                        CategoryId = course.Course.CategoryId,
                        PromotionId = course.Course.PromotionId,
                        RatingAvg = course.Course.Ratings.Any() ? course.Course.Ratings.Average(r => r.Start) : 0,
                        TotalRatings = course.Course.Ratings.Count()
                    }).ToList());
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
        public async Task<IActionResult> Post([FromBody] CartDetailsDTO cartDetailsDTO)
        {
            try
            {
                var orderDetail = new CartDetails
                {
                    CartId = cartDetailsDTO.CartId,
                    CourseId = cartDetailsDTO.CourseId
                };
                context.CartDetails.Add(orderDetail);
                context.SaveChanges();
                return Ok(orderDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CartDTO cartDto)
        {
            try
            {
                var updateCart = await cartRepository.GetById(id);
                if (updateCart != null)
                {
                    return Ok(mapper.Map<CartDTO>(await cartRepository.Update(id, mapper.Map<Cart>(cartDto))));
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
        }*/

        [HttpDelete("/api/cartDetails/bulkdelete")]
        public async Task<IActionResult> Delete([FromBody] CartDetailsDTO[] cartDetailsDTOs)
        {
            try
            {
                foreach (var item in cartDetailsDTOs)
                {
                    var course = context.CartDetails.SingleOrDefault(c => c.CartId == item.CartId && c.CourseId == item.CourseId);
                    context.Remove(course);
                    context.SaveChanges();
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
