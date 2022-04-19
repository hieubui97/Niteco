using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NitecoTest.Context;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Models;

namespace NitecoTest.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDataContext context) : base(context) { }

        public async Task<User> GetUser(string email, string password)
        {
            var user = await DataContext.Users.FirstOrDefaultAsync(x =>
                x.Email.Equals(email) && x.Password.Equals(password));

            return user;
        }

    }
}
