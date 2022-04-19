using System.Security.Cryptography;
using System.Text;

namespace NitecoTest.Helper
{
    public class HashHelper
    {
        public static string SHA256(string input)
        {
            SHA256 hash256 = System.Security.Cryptography.SHA256.Create();
            byte[] data = hash256.ComputeHash(Encoding.UTF8.GetBytes(input));
            var builder = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }

    }
}
