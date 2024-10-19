using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CartsController(MyDbContext db)
        {
            _db = db;
        }

        // GET: api/Carts/getallitems/{id}
        [HttpGet("getallitems/{id}")]
        public IActionResult GetAllProducts(int id)
        {
            var products = _db.Carts.Where(x => x.UserId == id).Select(x => new CartDTO
            {
                Id = x.Id,
                Quantity = x.Quantity,
                UserId = x.UserId,
                ProductId = x.ProductId,
                Product = new ProductDTO
                {
                    ProductName = x.Product.ProductName,
                    Image = x.Product.Image,
                    Price = x.Product.Price - (x.Product.Price * x.Product.Discount / 100)
                }
            }).ToList();

            return Ok(products);
        }
        // POST: api/Carts
        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartDTO cart)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == cart.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            decimal priceAfterDiscount = product.Discount.HasValue && product.Discount.Value > 0
                ? product.Price - (product.Price * (product.Discount.Value / 100))
                : product.Price;

            // Declare newCartItem outside the if-else block to ensure scope coverage
            Cart newCartItem = null;

            var existingCartItem = _db.Carts.FirstOrDefault(c => c.UserId == cart.UserId && c.ProductId == cart.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                existingCartItem.Price = priceAfterDiscount;
                _db.Carts.Update(existingCartItem);
            }
            else
            {
                newCartItem = new Cart
                {
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    Quantity = 1,
                    Price = priceAfterDiscount
                };

                _db.Carts.Add(newCartItem);
            }

            _db.SaveChanges();

            // Use existingCartItem if it's not null, otherwise use newCartItem
            return Ok(existingCartItem ?? newCartItem);
        }

        // PUT: api/Carts/cartitem/updateitem/{id}
        [HttpPut("cartitem/updateitem/{id}")]
        public IActionResult EditProduct(int id, [FromBody] UpdateCartDTO obj)
        {
            var cart = _db.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.Quantity = obj.Quantity;
            _db.Update(cart);
            _db.SaveChanges();
            return Ok();
        }

        // PUT: api/Carts/cartitem/updateMulti
        [HttpPut("cartitem/updateMulti")]
        public IActionResult UpdateCartItems([FromBody] List<UpdateCartDTO> cartItems)
        {
            foreach (var item in cartItems)
            {
                var cart = _db.Carts.FirstOrDefault(c => c.UserId == item.UserId && c.ProductId == item.ProductId);

                if (cart != null)
                {
                    cart.Quantity = item.Quantity;
                    _db.Carts.Update(cart);
                }
                else
                {
                    var newCartItem = new Cart
                    {
                        UserId = item.UserId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    _db.Carts.Add(newCartItem);
                }
            }

            _db.SaveChanges();
            return Ok(cartItems);
        }

        // DELETE: api/Carts/cartitem/deletitem/{id}
        [HttpDelete("cartitem/deletitem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _db.Carts.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _db.Carts.Remove(item);
            _db.SaveChanges();
            return Ok();
        }

        // GET: api/Carts/copon/{name}
        [HttpGet("copon/{name}")]
        public IActionResult CheckCoupon(string name)
        {
            var coupon = _db.Vouchers.SingleOrDefault(x => x.Code == name && x.ExpirationDate >= System.DateTime.Now && x.IsActive == true);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // GET: api/Carts/cartItemsSum/{id}
        [HttpGet("cartItemsSum/{id}")]
        public IActionResult CartItemsSum(int id)
        {
            var count = _db.Carts.Count(c => c.UserId == id);
            return Ok(count);
        }
    }
}
