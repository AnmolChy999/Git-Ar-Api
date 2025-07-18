namespace GitArApi.SongStoreApi.Repository.Configuration;

using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoDbClient
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoDbClient(
        ILoggerFactory loggerFactory,
        IOptionsMonitor<DocumentStoreConfiguration> configuration)
    {
        var settings = MongoClientSettings.FromConnectionString(configuration.CurrentValue.ConnectionString);
        settings.RetryReads = true;
        settings.RetryWrites = true;
        settings.MaxConnectionIdleTime = TimeSpan.FromMinutes(2);
        settings.MaxConnectionLifeTime = TimeSpan.FromMinutes(15);
        _client = new MongoClient(settings);
        _database = _client.GetDatabase(configuration.CurrentValue.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        var finalName = name ?? typeof(T).Name.ToLowerInvariant() + "s";
        return _database.GetCollection<T>(finalName);
    }

    public MongoClient GetClient()
    {
        return _client;
    }

    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}