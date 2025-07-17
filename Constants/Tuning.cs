using SongStoreApi.Constants;

public class Tuning
{
    public string Name { get; set; } = "E-Standard";
    public Note? LowE { get; set; } = Note.E;
    public Note? A { get; set; } = Note.A;
    public Note? D { get; set; } = Note.D;
    public Note? G { get; set; } = Note.G;
    public Note? B { get; set; } = Note.B;
    public Note? highE { get; set; } = Note.E;
}