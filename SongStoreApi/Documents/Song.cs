namespace GitArApi.SongStoreApi.Documents;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using GitArApi.SongStoreApi.Documents.Attributes;
using GitArApi.SongStoreApi.Constants;
[CollectionName("songs")]
public class Song : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public bool haveLearned { get; set; } = false;
    public Tuning Tuning { get; set; } = new();
}