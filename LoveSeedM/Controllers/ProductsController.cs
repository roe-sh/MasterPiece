using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.Models;
using System.Linq;
using System.Threading.Tasks;
using LoveSeedM.DTOs;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products
                .Join(_context.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) => new
                    {
                        id = product.Id,
                        productName = product.ProductName,
                        description = product.Description,
                        price = product.Price,
                        stockQuantity = product.StockQuantity,
                        image = product.Image,
                        discount = product.Discount,
                        categoryName = category.CategoryName
                    })
                .GroupJoin(_context.Reviews,
                    product => product.id,
                    review => review.ProductId,
                    (product, reviews) => new
                    {
                        product.id,
                        product.productName,
                        product.description,
                        product.price,
                        product.stockQuantity,
                        product.image,
                        product.discount,
                        product.categoryName,
                        reviews = reviews.DefaultIfEmpty()
                    })
                .SelectMany(p => p.reviews,
                    (p, review) => new
                    {
                        id = p.id,
                        productName = p.productName,
                        description = p.description,
                        price = p.price,
                        stockQuantity = p.stockQuantity,
                        image = p.image,
                        discount = p.discount,
                        categoryName = p.categoryName,
                        reviewRate = review != null ? review.Rating : (decimal?)null,
                        comment = review != null ? review.Comment : null,
                        status = review != null ? review.Status : null,
                    })
                .ToList();

            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Join(_context.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) => new
                    {
                        id = product.Id,
                        productName = product.ProductName,
                        description = product.Description,
                        price = product.Price,
                        stockQuantity = product.StockQuantity,
                        image = product.Image,
                        discount = product.Discount,
                        categoryName = category.CategoryName
                    })
                .GroupJoin(_context.Reviews,
                    product => product.id,
                    review => review.ProductId,
                    (product, reviews) => new
                    {
                        product.id,
                        product.productName,
                        product.description,
                        product.price,
                        product.stockQuantity,
                        product.image,
                        product.discount,
                        product.categoryName,
                        reviews = reviews.DefaultIfEmpty()
                    })
                .SelectMany(p => p.reviews,
                    (p, review) => new
                    {
                        id = p.id,
                        productName = p.productName,
                        description = p.description,
                        price = p.price,
                        stockQuantity = p.stockQuantity,
                        image = p.image,
                        discount = p.discount,
                        categoryName = p.categoryName,
                        reviewRate = review != null ? review.Rating : (decimal?)null,
                        comment = review != null ? review.Comment : null,
                        status = review != null ? review.Status : null,
                    })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequestDTO productDto)
        {
            // Validate the Category ID exists
            var category = await _context.Categories.FindAsync(productDto.CategoryId);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            // Save the image file to a folder
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            var imageFileName = Guid.NewGuid() + Path.GetExtension(productDto.Image.FileName);  // Create unique file name
            var imageFilePath = Path.Combine(imagesFolder, imageFileName);

            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

            // Create a new Product object from the DTO
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Discount = productDto.Discount,
                CategoryId = productDto.CategoryId,
                Image = imageFileName  // Store the unique image file name
            };

            // Add and save the new product to the database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return the created product information
            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Example fix in the DeleteProduct method
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Remove orders related to the specific product
            var orderItems = _context.OrderItems.Where(oi => oi.ProductId == id).ToList();
            if (orderItems.Any())
            {
                _context.OrderItems.RemoveRange(orderItems);
                await _context.SaveChangesAsync();
            }

            // Remove cart items related to the specific product
            var cartItems = _context.Carts.Where(c => c.ProductId == id).ToList();
            if (cartItems.Any())
            {
                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }

            // Remove reviews related to the specific product
            var reviews = _context.Reviews.Where(r => r.ProductId == id).ToList();
            if (reviews.Any())
            {
                _context.Reviews.RemoveRange(reviews);
                await _context.SaveChangesAsync();
            }

            // Finally, remove the product itself
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully", productId = id });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // GET: api/Products/discounted
        [HttpGet("discounted")]
        public IActionResult GetDiscountedProducts()
        {
            var discountedProducts = _context.Products
                .Where(p => p.Discount > 0)
                .Select(p => new
                {
                    p.Id,
                    p.ProductName,
                    p.Description,
                    p.Price,
                    p.StockQuantity,
                    p.Image,
                    p.Discount,
                    CategoryName = p.Category.CategoryName,
                    Reviews = p.Reviews.Select(r => new
                    {
                        r.Rating,
                        r.Comment,
                        r.Status
                    }).ToList()
                })
                .ToList();

            if (!discountedProducts.Any())
            {
                return NotFound();
            }

            return Ok(discountedProducts);
        }
    }
}
