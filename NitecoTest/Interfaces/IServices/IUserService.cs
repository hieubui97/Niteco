using System.Threading.Tasks;
using NitecoTest.Models;

namespace NitecoTest.Interfaces.IServices
{
    public interface IUserService
    {
        Task<User> ValidateUser(string email, string password);
    }
}
