using MongoDB.Driver;
using SongStoreApi.Documents.Attributes;
using SongStoreApi.Constants;

namespace SongStoreApi.Repository.Configuration;

public class MongoDbStore<T> where T : IDocument
{
    private readonly MongoDbClient _client;
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<MongoDbStore<T>> _logger;

    public MongoDbStore(
        ILogger<MongoDbStore<T>> logger,
        MongoDbClient client)
    {
        _logger = logger;
        _client = client;
        _collection = client.GetCollection<T>(GetCollectionName());
    }

    private string GetCollectionName()
    {
        if (typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault() is CollectionNameAttribute collectionNameAttribute)
        {
            return collectionNameAttribute.Name;
        }
        return typeof(T).Name.ToLowerInvariant() + "s";
    }
}
