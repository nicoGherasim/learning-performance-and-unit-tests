using Microsoft.AspNetCore.Mvc;
using ProductsApp.Application.Exceptions;
using ProductsApp.Application.Models;
using ProductsApp.Application.Services;

namespace ProductsApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(Guid id)
        {
            return Ok(this.productService.GetProductById(id));
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductRequestModel requestModel)
        {
            try
            {
                return Ok(this.productService.CreateProduct(requestModel));
            }
            catch (InputValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, UpdateProductRequestModel newProduct)
        {
            try
            {
                return Ok(this.productService.UpdateProduct(id, newProduct));
            }
            catch (InputValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
