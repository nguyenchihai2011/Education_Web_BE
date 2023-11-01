using AutoMapper;
using EducationAPI.Context;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StatisticalController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: api/<StatisticalController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int totalStudents = context.Students.Count();
            int totalLectures = context.Lectures.Count();
            int totalCourses = context.Courses.Count();
            int totalLessons = context.Lessons.Count();
            int totalOrders = context.Orders.Count();
            float totalRevenue = context.Orders.Sum(o => o.TotalPrice);

            return Ok(new
            {
                totalStudents = totalStudents,
                totalLectures = totalLectures,
                totalCourses = totalCourses,
                totalLessons = totalLessons,
                totalOrders = totalOrders,
                totalRevenue = totalRevenue
            });

        }

        [HttpGet("topcourses")]
        public IActionResult GetTopPurchasedCourses()
        {
            var topCourses = context.OrderDetails
                .GroupBy(od => od.CourseId)
                .Select(g => new
                {
                    Course = context.Courses.FirstOrDefault(c => c.Id == g.Key),
                    RatingAvg = context.Courses.FirstOrDefault(c => c.Id == g.Key).Ratings.Any() ? context.Courses.FirstOrDefault(c => c.Id == g.Key).Ratings.Average(r => r.Start) : 0,
                    TotalRatings = context.Courses.FirstOrDefault(c => c.Id == g.Key).Ratings.Count(),
                    TotalPurchases = g.Count()
                })
                .OrderByDescending(g => g.TotalPurchases)
                .Take(10)
                .ToList();

            return Ok(topCourses);
        }

        [HttpGet("toplectures/{month}")]
        public IActionResult GetTopInstructorsByRevenue(int month)
        {
            var topInstructors = context.OrderDetails
                .Where(od => od.Order.CreateAt.Month == month)
                .GroupBy(od => od.Course.Lecture.Id)
                .Select(g => new
                {
                    Instructor = context.Lectures.FirstOrDefault(l => l.Id == g.Key),
                    TotalRevenue = g.Sum(od => od.Price)
                })
                .OrderByDescending(g => g.TotalRevenue)
                .Take(10)
                .ToList();

            return Ok(topInstructors);
        }

        [HttpGet("revenue")]
        public IActionResult GetRevenueByMonth()
        {
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            List<float> monthlyRevenue = new List<float>(12);

            for (int i = 1; i <= 12; i++)
            {
                DateTime startDate = new DateTime(currentYear, i, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                float revenue = context.Orders
                    .Where(o => o.CreateAt >= startDate && o.CreateAt <= endDate)
                    .Sum(o => o.TotalPrice);

                monthlyRevenue.Add(revenue);
            }

            return Ok(monthlyRevenue);
        }


        [HttpGet("revenue/{lectureId}")]
        public IActionResult GetRevenueByLecture(int lectureId)
        {
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            List<float> monthlyRevenue = new List<float>(12);

            for (int i = 1; i <= 12; i++)
            {
                DateTime startDate = new DateTime(currentYear, i, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                float revenue = context.OrderDetails
                    .Where(od => od.CourseId == lectureId && od.Order.CreateAt >= startDate && od.Order.CreateAt <= endDate)
                    .Select(od => od.Price)
                    .Sum();

                monthlyRevenue.Add(revenue);
            }

            return Ok(monthlyRevenue);
        }

        // GET api/<StatisticalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StatisticalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatisticalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatisticalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
