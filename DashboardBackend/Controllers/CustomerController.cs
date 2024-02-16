using DashboardBackend.Data;
using DashboardBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DashboardBackend.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private APIResponse _response;

        public CustomerController(ApplicationContext db) 
        {
            _db = db;
            _response = new APIResponse();
        }

        // GET: api/<CustomerController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetCustomers()
        {
            try
            {
                IEnumerable<Customer> customers = await _db.Customers.ToListAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Successfully retrieved all customers";
                _response.Data = customers;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.Message = e.Message;
                return BadRequest(_response);
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult< APIResponse > > GetCustomerByID(int id)
        {
            try
            {
                Customer? customer = await _db.Customers.FirstOrDefaultAsync(customer => customer.ID == id);
                if (customer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = $"No customer found with ID {id}";
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = $"Successfully retrieved customer with ID {id}";
                _response.Data = customer;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.Message = e.Message;
                return BadRequest(_response);
            }
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult< APIResponse > > GetCustomerByName(string name)
        {
            try
            {
                name = name.ToLower();
                IEnumerable<Customer> customers = await _db.Customers.Where(
                     customer => customer.Name.Contains(name.ToLower())
                ).ToListAsync();

                if (customers.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = $"No customer found with name containing {name}";
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = $"Successfully retrieved customer with name containing {name}";
                _response.Data = customers;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.Message = e.Message;
                return BadRequest(_response);
            }
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
