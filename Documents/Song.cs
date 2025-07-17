using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SongStoreApi.Documents.Attributes;
using SongStoreApi.Constants;

namespace SongStoreApi.Documents;
[CollectionName("songs")]
public class Song : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public bool haveLearned { get; set; } = false;
    public ChordShape? Tuning { get; set; }
}