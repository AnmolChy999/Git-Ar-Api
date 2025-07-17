using MongoDB.Driver;
using SongStoreApi.Documents.Attributes;
using SongStoreApi.Constants;

namespace SongStoreApi.Repository.Configuration;

public class MongoDbStore<T> : IDocumentStore<T> where T : IDocument
{
    private readonly MongoDbClient _client;
    private readonly ILogger<MongoDbStore<T>> _logger;
    private readonly IMongoCollection<T> _collection;

    public MongoDbStore(
        ILogger<MongoDbStore<T>> logger,
        MongoDbClient client)
    {
        _logger = logger;
        _client = client;
        _collection = client.GetCollection<T>(GetCollectionName());
    }

    public async Task<T> GetDocumentAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> GetDocumentAsync(FilterDefinition<T> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
    }

    public async Task InsertAsync(T document, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(document, null, cancellationToken);
    }

    public async Task UpsertAsync(T document, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
        var replaceOptions = new ReplaceOptions { IsUpsert = true };
        await _collection.ReplaceOneAsync(filter, document, replaceOptions, cancellationToken);
    }

    public async Task UpdateAsync(T document, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(document.Id))
        {
            throw new Exception("Cant insert a document with empty id.");
        }

        var filter = Builders<T>.Filter.Eq(f => f.Id, document.Id);
        var options = new ReplaceOptions
        {
            IsUpsert = false,
            BypassDocumentValidation = false,
        };
        try
        {
            var saveTask = _collection.ReplaceOneAsync(filter, document, options, cancellationToken);
            var result = await saveTask.ConfigureAwait(false);
            if (!result.IsAcknowledged)
            {
                throw new Exception("Update was not acknowledged");
            }

            if (result.ModifiedCount == 1)
            {
                return;
            }

            throw new Exception("Failed to update the document");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
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
