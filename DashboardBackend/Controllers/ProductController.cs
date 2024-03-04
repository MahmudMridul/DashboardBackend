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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private APIResponse _response;

        public ProductController(ApplicationContext db)
        {
            _db = db;
            _response = new APIResponse();
        }
        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetProducts()
        {
            try
            {
                IEnumerable<Product> products = await _db.Products.ToListAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Successfully retrieved all products";
                _response.Data = products;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.Message = e.Message;
                return BadRequest(_response);
            }
        }

        [HttpGet("totalstock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetTotalStock()
        {
            try
            {
                int totalStock = await _db.Products.SumAsync((product) => product.Stock );
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Successfully retrieved total stock";
                _response.Data = totalStock;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.Message = e.Message;
                return BadRequest(_response);
            }
        }

        // GET api/<ProductController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProductController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
