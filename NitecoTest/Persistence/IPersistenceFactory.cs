using NitecoTest.Configs;

namespace NitecoTest.Persistence
{
    public interface IPersistenceFactory
    {
        IJwtConfig GetJwtConfig();
    }
}
