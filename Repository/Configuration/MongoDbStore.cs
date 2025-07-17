using MongoDB.Driver;

public class MongoDBStore<T> where T : IDocument
{
    private readonly MongoDbClient _client;
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<MongoDBStore<T>> _logger;
    private readonly string _typeValue;
    private readonly bool _useType;

    public MongoDbStore(
        ILogger<MongoDBStore<T>> logger,
        MongoDbClient client)
    {
        _logger = logger;
        _client = client;

        SetupCollection(client).GetAwaiter().GetResult();
        _collection = client.GetCollection<T>(GetCollectionName());

        // Attributes needed for this document
        var useTypeAttribute = (UseDocumentTypeForQueryAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(UseDocumentTypeForQueryAttribute)) ?? new UseDocumentTypeForQueryAttribute();
        _useType = useTypeAttribute.Value;
        _typeValue = useTypeAttribute.Name ?? typeof(TDocument).Name;
        SetupIndexes();
    }
}
