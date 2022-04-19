using NitecoTest.Configs;

namespace NitecoTest.Persistence
{
    public class PersistenceFactory : IPersistenceFactory
    {
        public JwtConfig JwtConfig { get; set; }

        public PersistenceFactory(JwtConfig jwtConfig)
        {
            JwtConfig = jwtConfig;
        }

        public IJwtConfig GetJwtConfig()
        {
            return JwtConfig;
        }
    }
}
