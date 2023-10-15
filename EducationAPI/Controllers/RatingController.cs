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
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IRatingRepository ratingRepository;
        private readonly IMapper mapper;

        public RatingController(ApplicationDbContext context, IRatingRepository ratingRepository, IMapper mapper)
        {
            this.context = context;
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RatingDTO>> Get()
        {
            return mapper.Map<IEnumerable<RatingDTO>>(await ratingRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var rating = await ratingRepository.GetById(id);
                if (rating != null)
                {
                    return Ok(mapper.Map<RatingDTO>(rating));
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
        public async Task<IActionResult> Post([FromBody] RatingDTO ratingDto)
        {
            try
            {
                var newRating = await ratingRepository.Add(mapper.Map<RatingEntity>(ratingDto));
                return Ok(mapper.Map<RatingDTO>(newRating));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RatingDTO ratingDto)
        {
            try
            {
                var updateRating = await ratingRepository.GetById(id);
                if (updateRating != null)
                {
                    return Ok(mapper.Map<RatingDTO>(await ratingRepository.Update(id, mapper.Map<RatingEntity>(ratingDto))));
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
                var rating = await ratingRepository.GetById(id);
                if (rating != null)
                {
                    await ratingRepository.Delete(rating);
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
