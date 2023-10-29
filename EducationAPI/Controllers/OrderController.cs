using AutoMapper;
using EducationAPI.Context;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OrderController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDTO orderModel) // OrderModel là model chứa thông tin cần thiết để tạo hóa đơn và chi tiết hóa đơn
        {
            // Thực hiện kiểm tra hợp lệ của dữ liệu đầu vào
            // Tạo một đối tượng Order từ dữ liệu đầu vào
            Order order = new Order();
            order = mapper.Map<Order>(orderModel);

            // Thêm Order vào cơ sở dữ liệu
            context.Orders.Add(order);
            context.SaveChanges();

            // Thêm OrderDetails vào cơ sở dữ liệu
            foreach (var orderDetailModel in orderModel.OrderDetailsDTO)
            {
                var orderDetail = new OrderDetails
                {
                    Price = orderDetailModel.Price,
                    OrderId = order.Id,
                    CourseId = orderDetailModel.CourseId
                };
                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
            }
            foreach (var orderDetailModel in orderModel.OrderDetailsDTO)
            {
                var lessons = context.Lessons.Where(x => x.Section.Course.Id == orderDetailModel.CourseId).ToList();
                foreach (var lesson in lessons)
                {
                    var studentLesson = new StudentLesson
                    {
                        StudentId = order.StudentId,
                        LessonId = lesson.Id,
                        IsLock = true
                    };
                    context.StudentLessons.Add(studentLesson);
                    context.SaveChanges();
                }
            }
            // Trả về mã trạng thái thành công và thông tin về đối tượng đã tạo
            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
