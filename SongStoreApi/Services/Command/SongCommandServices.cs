namespace GitArApi.SongStoreApi.Services.Command;

using GitArApi.SongStoreApi.Contracts;
using GitArApi.SongStoreApi.Documents;
using GitArApi.SongStoreApi.Repository;
using GitArApi.SongStoreApi.Services.Command.Abstractions;

public class SongCommandServices : ISongCommandServices
{
    private readonly IDocumentStore<Song> _songStore;

    public SongCommandServices(IDocumentStore<Song> songStore)
    {
        _songStore = songStore;
    }

    public async Task AddSongAsync(CreateSongRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "CreateSongRequest cannot be null");
        }
        Song song = new()
        {
            Title = request.Title,
            Artist = request.Artist,
            haveLearned = request.haveLearned,
            Tuning = request.Tuning
        };
        await _songStore.InsertAsync(song, cancellationToken);
    }
}