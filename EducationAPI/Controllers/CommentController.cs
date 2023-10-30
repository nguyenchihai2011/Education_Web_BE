using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
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
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        public CommentController(ApplicationDbContext context, ICommentRepository commentRepository, IMapper mapper)
        {
            this.context = context;
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDTO>> Get()
        {

            return mapper.Map<IEnumerable<CommentDTO>>(await commentRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var comment = await commentRepository.GetById(id);
                if (comment != null)
                {
                    return Ok(mapper.Map<CommentDTO>(comment));
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

        [HttpGet("/api/getComments")]
        public async Task<IActionResult> getComments(int courseId)
        {
            try
            {
                var comments = context.Comments
                    .Include(c => c.AppUser).ThenInclude(a => a.Student)
                    .Include(c => c.AppUser).ThenInclude(a => a.Lecture)
                    .Where(c => c.CourseId == courseId).Select(c => new
                {
                    Id = c.Id,
                    Content = c.Content,
                    Name = c.AppUser.UserType == "Student" ? c.AppUser.Student.FirstName + " " + c.AppUser.Student.LastName : c.AppUser.Lecture.FirstName + " " + c.AppUser.Lecture.LastName,
                    AvatarUrl = c.AppUser.UserType == "Student" ? c.AppUser.Student.AvatarUrl : c.AppUser.Lecture.AvatarUrl
                    }).ToList();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentDTO commentDto)
        {
            try
            {
                var newComment = await commentRepository.Add(mapper.Map<Comment>(commentDto));
                return Ok(mapper.Map<CommentDTO>(newComment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentDTO commentDto)
        {
            try
            {
                var updateComment = await commentRepository.GetById(id);
                if (updateComment != null)
                {
                    return Ok(mapper.Map<CommentDTO>(await commentRepository.Update(id, mapper.Map<Comment>(commentDto))));
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
                var comment = await commentRepository.GetById(id);
                if (comment != null)
                {
                    await commentRepository.Delete(comment);
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
