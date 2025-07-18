namespace GitArApi.SongStoreApi.Documents;

using GitArApi.SongStoreApi.Constants;

public class ChordShape : IDocument
{
    public string? Id { get; set; }
    public String? ChordName { get; set; }

    public Note? LowE { get; set; }

    public Note? A { get; set; }

    public Note? D { get; set; }

    public Note? G { get; set; }

    public Note? B { get; set; }

    public Note? HighE { get; set; }
}
