
namespace GitArApi.SongStoreApi.Services.Query;

using GitArApi.SongStoreApi.Documents;
using GitArApi.SongStoreApi.Services.Query.Abstractions;
using GitArApi.Common.Mongo;

public class SongQueryServices : ISongQueryServices
{
    private readonly IDocumentStore<Song> _songStore;

    public SongQueryServices(IDocumentStore<Song> songStore)
    {
        _songStore = songStore;
    }
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

    public async Task<Song> GetSongByIdAsync(string id, CancellationToken cancellationToken)
    {
        var song = await _songStore.GetDocumentAsync(id, cancellationToken);
        if (song == null)
        {
            return new();
        }
        return song;
    }

}