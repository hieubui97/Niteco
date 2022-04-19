using System.Threading.Tasks;

namespace NitecoTest.JWTClient
{
    public interface IJwtClient
    {
        Task<string> GetJwtToken(string email);
    }
}
