using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NitecoTest.Context;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Models;

namespace NitecoTest.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDataContext context) : base(context) { }

        public async Task<List<Order>> GetAllAsync()
        {
            var allOrder = await DataContext.Orders.Include(o => o.Customer).Include(o => o.Product).ToListAsync();

            return allOrder;
        }
    }
}
