using DashboardBackend.Data;
using DashboardBackend.Models;
using DashboardBackend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
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

        [HttpPost("stockupdate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateStock(ProductStockUpdateDTO productDTO)
        {
            if (productDTO == null || productDTO.Stock < 0 || productDTO.Price < 0)
            {
                _response.Message = "Parameters are invalid";
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            string[] categories = ["", "Men", "Women", "Children", "Sports", "Graphic"];
            string[] sizes = ["", "S", "M", "L", "XL", "XXL"];
            string[] colors = ["", "Red", "Blue", "Yellow", "Black", "White"];
            try
            {
                for (int cat = 0; cat < productDTO.Categories.Length; ++cat)
                {
                    for (int s = 0; s < productDTO.Sizes.Length; ++s)
                    {
                        for (int c = 0; c < productDTO.Colors.Length; ++c)
                        {
                            string name = $"{categories[(int)productDTO.Categories[cat]]} " +
                                $"{sizes[(int)productDTO.Sizes[s]]} " + $"{colors[(int)productDTO.Colors[c]]}";
                            Product? product = await _db.Products.SingleOrDefaultAsync((p) => p.Name == name);

                            if(product == null)
                            {
                                _response.Message += $"{name} is an invalid product name\n";
                            }
                            else
                            {
                                int stock = productDTO.IsAdd ? product.Stock + productDTO.Stock : product.Stock - productDTO.Stock;
                                if(stock < 0)
                                {
                                    _response.Message += $"Invalid stock update for {name}";
                                }
                                else
                                {
                                    product.Stock = stock;
                                    product.Price = productDTO.Price;
                                    _db.Products.Update(product);
                                }
                            }

                        }
                    }
                }
                _response.Message += "Update successfull";
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                await _db.SaveChangesAsync();
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
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
