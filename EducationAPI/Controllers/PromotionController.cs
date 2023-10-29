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
    public class PromotionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IPromotionRepository promotionRepository;
        private readonly IMapper mapper;

        public PromotionController(ApplicationDbContext context, IPromotionRepository promotionRepository, IMapper mapper)
        {
            this.context = context;
            this.promotionRepository = promotionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PromotionDTO>> Get()
        {
            return mapper.Map<IEnumerable<PromotionDTO>>(await promotionRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var promotion = await promotionRepository.GetById(id);
                if (promotion != null)
                {
                    return Ok(mapper.Map<PromotionDTO>(promotion));
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
        public async Task<IActionResult> Post([FromBody] PromotionDTO promotionDto)
        {
            try
            {
                var newPromotion = await promotionRepository.Add(mapper.Map<Promotion>(promotionDto));
                return Ok(mapper.Map<PromotionDTO>(newPromotion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PromotionDTO promotionDto)
        {
            try
            {
                var updatePromotion = await promotionRepository.GetById(id);
                if (updatePromotion != null)
                {
                    return Ok(mapper.Map<PromotionDTO>(await promotionRepository.Update(id, mapper.Map<Promotion>(promotionDto))));
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
                var promotion = await promotionRepository.GetById(id);
                if (promotion != null)
                {
                    await promotionRepository.Delete(promotion);
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
