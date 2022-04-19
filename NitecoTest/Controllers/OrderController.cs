using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NitecoTest.Context;
using NitecoTest.Interfaces.IServices;
using NitecoTest.Models;
using NitecoTest.Models.Request.Order;

namespace NitecoTest.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDataContext _context;
        private readonly IOrderService _orderService;

        public OrderController(AppDataContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var allOrder = await _orderService.GetAll();
            return View(allOrder);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            var order = new Order()
            {
                ProductId = request.ProductId,
                CustomerId = request.CustomerId,
                Amount = request.Amount,
                OrderDate = request.OrderDate
            };

            if (ModelState.IsValid)
            {
                var result = await _orderService.CreateOrder(order);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Amount", "The amount of the order is greater than the quantity of the product");
                }
            }


            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", order.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", order.ProductId);
            return View(order);
        }
    }
}
