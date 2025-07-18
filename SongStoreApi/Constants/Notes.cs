namespace SongStoreApi.Constants;
public enum Note
{
    A = 0,
    ASharp = 1,
    Bb = 1,
    B = 2,
    C = 3,
    CSharp = 4,
    Db = 4,
    D = 5,
    DSharp = 6,
    Eb = 6,
    E = 7,
    F = 8,
    FSharp = 9,
    Gb = 9,
    G = 10,
    GSharp = 11,
    Ab = 11
}



public enum StandardNotes
{
    A = Note.A,
    ASharp = Note.ASharp,
    B = Note.B,
    C = Note.C,
    CSharp = Note.CSharp,
    D = Note.D,
    DSharp = Note.DSharp,
    E = Note.E,
    F = Note.F,
    FSharp = Note.FSharp,
    G = Note.G,
    GSharp = Note.GSharp
}

public enum FlatNotes
{
    A = Note.A,
    BFlat = Note.Bb,
    B = Note.B,
    C = Note.C,
    DFlat = Note.Db,
    D = Note.D,
    EFlat = Note.Eb,
    E = Note.E,
    F = Note.F,
    GFlat = Note.Gb,
    G = Note.G,
    AFlat = Note.Ab
}