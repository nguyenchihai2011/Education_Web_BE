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
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartController(ApplicationDbContext context, ICartRepository cartRepository, IMapper mapper)
        {
            this.context = context;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CartDTO>> Get()
        {
            return mapper.Map<IEnumerable<CartDTO>>(await cartRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cart = await cartRepository.GetById(id);
                if (cart != null)
                {
                    return Ok(mapper.Map<CartDTO>(cart));
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
        public async Task<IActionResult> Post([FromBody] CartDTO cartDto)
        {
            try
            {
                var newCart = await cartRepository.Add(mapper.Map<CartEntity>(cartDto));
                return Ok(mapper.Map<CartDTO>(newCart));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CartDTO cartDto)
        {
            try
            {
                var updateCart = await cartRepository.GetById(id);
                if (updateCart != null)
                {
                    return Ok(mapper.Map<CartDTO>(await cartRepository.Update(id, mapper.Map<CartEntity>(cartDto))));
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
                var cart = await cartRepository.GetById(id);
                if (cart != null)
                {
                    await cartRepository.Delete(cart);
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
    }
}
