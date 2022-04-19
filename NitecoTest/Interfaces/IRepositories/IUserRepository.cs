using System.Threading.Tasks;
using NitecoTest.Models;

namespace NitecoTest.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string email, string password);
    }
}
