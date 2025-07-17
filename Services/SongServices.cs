
using SongStoreApi.Documents;

public class SongServices
{
    public HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken)
    {
        var songs = new HashSet<Song>();
        return songs;
    }
}