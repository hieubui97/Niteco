using System;

namespace NitecoTest.Configs
{
    public interface IJwtConfig : IDisposable
    {
        JwtConfig GetJwtConfig();
    }
}
