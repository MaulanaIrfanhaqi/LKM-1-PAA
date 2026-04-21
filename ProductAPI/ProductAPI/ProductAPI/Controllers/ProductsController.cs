using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var data = await _context.Products.Include(p => p.Category).ToListAsync();
            return Ok(new { status = "success", data });
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound(new { status = "error", message = "Produk tidak ditemukan" });

            return Ok(new { status = "success", data = product });
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest(new { status = "error", message = "Data tidak boleh kosong" });
                }

                var category = await _context.Categories.FindAsync(product.CategoryId);
                if (category == null)
                {
                    return BadRequest(new { status = "error", message = "Category tidak ditemukan" });
                }

                product.Id = 0;
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return Ok(new { status = "success", data = product });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Terjadi kesalahan pada server",
                    detail = ex.Message
                });
            }
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product input)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound(new { status = "error", message = "Produk tidak ditemukan" });

            product.Name = input.Name;
            product.Price = input.Price;
            product.CategoryId = input.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { status = "success", data = product });
        }

        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound(new { status = "error", message = "Produk tidak ditemukan" });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { status = "success", message = "Produk berhasil dihapus" });
        }
    }
}