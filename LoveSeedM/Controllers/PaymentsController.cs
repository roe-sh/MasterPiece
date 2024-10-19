﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.Models;
using LoveSeedM.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PaymentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // POST: api/Payments/AddToPaymentTableByOrderId/{id}
        [HttpPost("AddToPaymentTableByOrderId/{id}")]
        public ActionResult AddToPaymentTable(int id, [FromBody] PaymentsDTO paymentsDTO)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Creating a new Payment object based on the provided DTO and default values
            var payment = new Payment
            {
                OrderId = id,
                PaymentDate = DateTime.Now,
                PaymentMethod = !string.IsNullOrEmpty(paymentsDTO.PaymentMethod) ? paymentsDTO.PaymentMethod : "Paypal",
                PaymentAmount = paymentsDTO.PaymentAmount,
                TransactionId = !string.IsNullOrEmpty(paymentsDTO.TransactionId) ? paymentsDTO.TransactionId : Guid.NewGuid().ToString(),
                PaymentStatus = !string.IsNullOrEmpty(paymentsDTO.PaymentStatus) ? paymentsDTO.PaymentStatus : "Completed"
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return Ok(new { Message = "Payment successfully added to the table.", PaymentId = payment.Id });
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
