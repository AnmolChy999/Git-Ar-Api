using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GitArApi.SongStoreApi.Constants;
public class Tuning
{
    public string Name { get; set; } = "E-Standard";

    [BsonRepresentation(BsonType.String)]
    public Note? LowE { get; set; } = Note.E;

    [BsonRepresentation(BsonType.String)]
    public Note? A { get; set; } = Note.A;

    [BsonRepresentation(BsonType.String)]
    public Note? D { get; set; } = Note.D;

    [BsonRepresentation(BsonType.String)]
    public Note? G { get; set; } = Note.G;

    [BsonRepresentation(BsonType.String)]
    public Note? B { get; set; } = Note.B;
    
    [BsonRepresentation(BsonType.String)]
    public Note? highE { get; set; } = Note.E;
}