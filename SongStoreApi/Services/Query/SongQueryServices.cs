
using SongStoreApi.Documents;
using SongStoreApi.Services.Query.Abstractions;

namespace SongStoreApi.Services.Query;

public class SongQueryServices : ISongQueryServices
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