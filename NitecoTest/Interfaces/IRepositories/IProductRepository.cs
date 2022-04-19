using System.Collections.Generic;
using System.Threading.Tasks;
using NitecoTest.Models;

namespace NitecoTest.Interfaces.IRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByIdAsync(int id);
    }
}
