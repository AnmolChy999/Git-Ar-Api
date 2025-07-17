
using SongStoreApi.Documents;
using SongStoreApi.Services.Abstractions;

namespace SongStoreApi.Services;
public class SongServices : ISongServices
{
    public HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken)
    {
        var songs = new HashSet<Song>();
        var oneSong = new Song
        {
            Id = new Guid().ToString(),
            Title = songName,
            Artist = "Unknown Artist",
            haveLearned = false,
            Tuning = new()
        };
        songs.Add(oneSong);
        return songs;
    }
}