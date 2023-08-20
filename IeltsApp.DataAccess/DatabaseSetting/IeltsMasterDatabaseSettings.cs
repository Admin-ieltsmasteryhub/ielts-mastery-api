namespace IeltsApp.DataAccess
{
    public class IeltsMasterDatabaseSettings : IIeltsMasterDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
    }
}