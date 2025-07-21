using GitArApi.SongStoreApi.Constants;

namespace GitArApi.SongStoreApi.Contracts;

public class CreateSongRequest
{
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public bool haveLearned { get; set; } = false;
    public Tuning Tuning { get; set; } = new();
}