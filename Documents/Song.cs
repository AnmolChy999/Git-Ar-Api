using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Song : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public bool haveLearned { get; set; } = false;
    public ChordShape? Tuning { get; set; }
}