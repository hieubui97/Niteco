using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NitecoTest.Context;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Models;

namespace NitecoTest.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDataContext context) : base(context) { }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await DataContext.Products.FirstOrDefaultAsync(x => x.Id.Equals(id));

            return product;
        }
    }
}
