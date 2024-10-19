using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Linq;
using project7.DTOs;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OrdersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders/GetCart/{id}
        [HttpGet("GetCart/{id}")]
        public ActionResult GetCartByUserId(int id)
        {
            var cartItems = _context.Carts.Where(x => x.UserId == id)
                .Include(x => x.Product)  // Include product details
                .Select(x => new CartRequestDTO
                {
                    UserId = x.UserId ?? 0,
                    ProductId = x.ProductId ?? 0,
                    Quantity = x.Quantity,
                    ProductDTO = new ProductDTO
                    {
                        ProductName = x.Product.ProductName,
                        Price = x.Product.Price
                    }
                }).ToList();

            return Ok(cartItems);
        }

        // POST: api/Orders/GetUserAddress
        [HttpPost("GetUserAddress")]
        public IActionResult GetUserAddress([FromForm] AddressDTO addressDTO)
        {
            var address = new Address
            {
                UserId = addressDTO.UserId,
                AddressLine = addressDTO.AddressLine,
                City = addressDTO.City,
                Country = addressDTO.Country,
                PhoneNumber = addressDTO.PhoneNumber,
                PostalCode = addressDTO.PostalCode
            };

            _context.Addresses.Add(address);
            _context.SaveChanges();
            return Ok();
        }

        // GET: api/Orders/getaddress/{id}
        [HttpGet("getaddress/{id}")]
        public ActionResult GetUserAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(x => x.UserId == id);
            if (address == null)
            {
                return NotFound("User Address not found.");
            }
            return Ok(address);
        }

        // GET: api/Orders/GetAllOrdersByUserEmail/{email}
        [HttpGet("GetAllOrdersByUserEmail/{email}")]
        public IActionResult GetAllOrdersByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Vouchers)
                .Where(o => o.UserId == user.Id)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId ?? 0,
                        ProductName = oi.Product.ProductName,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList(),
                    UserOrderDTO = new UserOrderDTO
                    {
                        Username = o.User.Username,
                    },
                    Vouchers = o.Vouchers != null ? new VoucherOrderDTO
                    {
                        Code = o.Vouchers.FirstOrDefault().Code,
                        DiscountAmount = o.Vouchers.FirstOrDefault().DiscountAmount
                    } : null
                }).ToList();

            return Ok(orders);
        }

        // GET: api/Orders/GetAllOrdersByUserId/{id}
        [HttpGet("GetAllOrdersByUserId/{id}")]
        public IActionResult GetAllOrdersByUserId(int id)
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Where(o => o.UserId == id)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId ?? 0,
                        ProductName = oi.Product.ProductName,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList(),
                    UserOrderDTO = new UserOrderDTO
                    {
                        Username = o.User.Username,
                    }
                }).ToList();

            return Ok(orders);
        }

        // GET: /AllOrders/
        [HttpGet("/AllOrders/")]
        public IActionResult GetAllOrders()
        {
            var orders = _context.Orders
                .Join(_context.Users,
                    o => o.UserId,
                    u => u.Id,
                    (o, u) => new
                    {
                        o.Id,
                        u.Username,
                        u.Email,
                        o.Status,
                        o.TotalAmount,
                        o.OrderDate
                    })
                .OrderBy(r => r.Status == "Pending" ? 0 : r.Status == "Shipped" ? 1 : r.Status == "Delivered" ? 2 : 3)
                .ToList();

            return Ok(orders);
        }

        // POST: api/Orders/AddNewOrderByUserId
        [HttpPost("AddNewOrderByUserId")]
        public IActionResult AddNewOrder([FromBody] AddNewOrderByUserIdDTO newOrder)
        {
            var cartItems = _context.Carts.Where(c => c.UserId == newOrder.UserId).ToList();

            if (!cartItems.Any())
            {
                return BadRequest("Cart is empty.");
            }

            var order = new Order
            {
                UserId = newOrder.UserId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cartItems.Sum(item => item.Quantity * item.Product.Price)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();

            return Ok(order);
        }

        // GET: api/Orders/GetLastOrderIdByUserId/{id}
        [HttpGet("GetLastOrderIdByUserId/{id}")]
        public IActionResult GetLastOrderIdByUserId(int id)
        {
            var lastOrder = _context.Orders.Where(o => o.UserId == id)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();

            return Ok(lastOrder);
        }

        // GET: api/Orders/GetAllOrdersByOrderDate/{orderdate}
        [HttpGet("GetAllOrdersByOrderDate/{orderdate}")]
        public IActionResult GetOrdersByOrderDate(DateTime orderdate)
        {
            var orders = _context.Orders.Where(o => EF.Functions.DateDiffDay(o.OrderDate, orderdate) == 0)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId ?? 0,
                        ProductName = oi.Product.ProductName,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList(),
                    UserOrderDTO = new UserOrderDTO
                    {
                        Username = o.User.Username,
                    }
                }).ToList();

            return Ok(orders);
        }
    }
}
