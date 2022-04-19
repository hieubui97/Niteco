using System.Collections.Generic;
using System.Threading.Tasks;
using NitecoTest.Models;

namespace NitecoTest.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();

        Task<bool> CreateOrder(Order order);
    }
}
