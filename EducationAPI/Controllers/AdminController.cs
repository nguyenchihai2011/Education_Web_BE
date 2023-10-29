using AutoMapper;
using EducationAPI.Context;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAdminRepository adminRepository;
        private readonly IMapper mapper;

        public AdminController(ApplicationDbContext context, IAdminRepository adminRepository, IMapper mapper)
        {
            this.context = context;
            this.adminRepository = adminRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AdminDTO>> Get()
        {
            return mapper.Map<IEnumerable<AdminDTO>>(await adminRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var admin = await adminRepository.GetById(id);
                if (admin != null)
                {
                    return Ok(mapper.Map<AdminDTO>(admin));
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
        public async Task<IActionResult> Post([FromBody] AdminDTO adminDto)
        {
            try
            {
                var admin = mapper.Map<Admin>(adminDto);
                if(context.Admins.SingleOrDefault(a => a.UserId == admin.UserId) == null
                    && context.Lectures.SingleOrDefault(a => a.UserId == admin.UserId) == null
                    && context.Students.SingleOrDefault(a => a.UserId == admin.UserId) == null)
                {
                    await adminRepository.Add(admin);
                    return Ok(new ApiResponse { Success = true, Message = "Created success" });    
                }
                return BadRequest(
                        new ApiResponse { Success = false, Message = "UserId is exist" }
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AdminDTO admin)
        {
            try
            {
                var updateAdmin = await adminRepository.GetById(id);
                if (updateAdmin != null)
                {
                    return Ok(mapper.Map<AdminDTO>(await adminRepository.Update(id, mapper.Map<Admin>(admin))));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var admin = await adminRepository.GetById(id);
                if (admin != null)
                {
                    await adminRepository.Delete(admin);
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
