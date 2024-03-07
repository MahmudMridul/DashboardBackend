using DashboardBackend.Data;
using DashboardBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DashboardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private APIResponse _response;

        public OrderController(ApplicationContext db)
        {
            _db = db;
            _response = new APIResponse();
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task< ActionResult< APIResponse > > GetAllOrders() 
        {
            try
            {
                IEnumerable<Order> orders = await _db.Orders.ToListAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Successfully retrieved all orders";
                _response.Data = orders;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.Message = e.Message;
                return BadRequest(_response);
            }
        }

        //// GET api/<OrderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<OrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<OrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
