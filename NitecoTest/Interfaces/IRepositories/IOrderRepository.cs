using System.Collections.Generic;
using System.Threading.Tasks;
using NitecoTest.Models;

namespace NitecoTest.Interfaces.IRepositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetAllAsync();
    }
}
