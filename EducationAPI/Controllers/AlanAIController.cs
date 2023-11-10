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
    public class AlanAIController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AlanAIController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/alanai/most-purchase")]
        public IActionResult Get(string categoryName)
        {
            var mostPurchasedCourses = context.OrderDetails.Where(od => od.Course.Category.Name == categoryName)
            .GroupBy(od => od.CourseId)
            .OrderByDescending(group => group.Count())
            .Select(group => new
            {
                CourseId = group.Key,
                PurchaseCount = group.Count()
            }).FirstOrDefault();

            // Trả về danh sách khoá học được mua nhiều nhất
            return Ok(mostPurchasedCourses);
        }

        [HttpGet("/api/alanai/top-rating")]
        public IActionResult GetTopRatedCourses(string categoryName)
        {
            var topRatedCourses = context.Courses.Where(c => c.Category.Name == categoryName)
                .OrderByDescending(course => course.Ratings
                    .Average(rating => rating.Start))
                .FirstOrDefault();

            // Trả về danh sách khoá học có lượt đánh giá cao nhất
            return Ok(topRatedCourses);
        }
    }
}
