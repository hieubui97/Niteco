using System;
using System.Threading.Tasks;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Interfaces.IServices;
using NitecoTest.JWTClient;
using NitecoTest.Models;

namespace NitecoTest.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            try
            {
                var hashPassword = Helper.HashHelper.SHA256(password);

                var user = await _userRepository.GetUser(email, hashPassword);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
