namespace NitecoTest.Configs
{
    public class RdsConfiguration
    {
        public string ConnectionString { get; set; }

        public RdsConfiguration(string connectionString) { }

        public RdsConfiguration(string server, string databaseName, string userName, string password, string options)
        {
            this.ConnectionString = string.Format($"Server = {server}; Database = {databaseName}; User Id = {userName}; Password = {password}; {options} ");
        }
    }
}
