using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Interfaces.IServices;
using NitecoTest.JWTClient;
using NitecoTest.Repositories;
using NitecoTest.Services;

namespace NitecoTest
{
    public static class DIExtenstion
    {
        public static IList<IServiceCollection> DIRegister(this IServiceCollection services)
        {
            IList<IServiceCollection> listDIRegister = new List<IServiceCollection>
            {
                //
                services.AddTransient<IJwtClient, JwtClient>(),

                //INJECTION THE SERVICE HERE
                services.AddScoped<IUserService, UserService>(),
                services.AddScoped<IOrderService, OrderService>(),

                //INJECTION THE REPOSITORY HERE
                services.AddTransient<IUserRepository, UserRepository>(),
                services.AddTransient<IOrderRepository, OrderRepository>(),
                services.AddTransient<IProductRepository, ProductRepository>(),
            };
            return listDIRegister;
        }
    }
}
