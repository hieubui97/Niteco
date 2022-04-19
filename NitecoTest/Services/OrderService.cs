using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Interfaces.IServices;
using NitecoTest.Models;

namespace NitecoTest.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Order>> GetAll()
        {
            var allOrder = await _orderRepository.GetAllAsync();

            return allOrder;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(order.ProductId);

                if (product == null || order.Amount > product.Quantity)
                {
                    return false;
                }
                await _orderRepository.Add(order);

                product.Quantity -= order.Amount;
                await _productRepository.Update(product);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
