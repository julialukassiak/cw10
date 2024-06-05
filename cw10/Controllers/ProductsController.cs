using Microsoft.AspNetCore.Mvc;
using cw10.Data;
using cw10.DTO;
using cw10.Models;

namespace cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = new Product
                {
                    Name = dto.ProductName,
                    Weight = dto.ProductWeight,
                    Width = dto.ProductWidth,
                    Height = dto.ProductHeight,
                    Depth = dto.ProductDepth
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var productCategories = dto.ProductCategories.Select(catId => new ProductCategory
                {
                    ProductId = product.ProductId,
                    CategoryId = catId
                }).ToList();

                _context.ProductCategories.AddRange(productCategories);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetProductById), new { productId = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas tworzenia produktu: {ex.Message}");
            }
        }
    }
}
