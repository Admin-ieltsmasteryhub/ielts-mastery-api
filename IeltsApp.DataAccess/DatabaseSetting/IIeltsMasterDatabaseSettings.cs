namespace IeltsApp.DataAccess
{
    public interface IIeltsMasterDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}