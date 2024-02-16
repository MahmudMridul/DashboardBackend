using DashboardBackend.Data;
using DashboardBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DashboardBackend.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public CustomerController(ApplicationContext db) 
        {
            _db = db;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _db.Customers.ToListAsync();
        }

        // GET api/<CustomerController>/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult< Customer > > GetCustomerByID(int id)
        {
            Customer? customer = await _db.Customers.FirstOrDefaultAsync(customer => customer.ID == id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerByName(string name)
        {
            name = name.ToLower();
            return await _db.Customers.Where(
                 customer =>  customer.Name.Contains(name.ToLower())
            ).ToListAsync();
        }

        //// POST api/<CustomerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CustomerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CustomerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
