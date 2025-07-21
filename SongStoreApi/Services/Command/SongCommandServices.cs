
using GitArApi.SongStoreApi.Contracts;
using GitArApi.SongStoreApi.Documents;
using GitArApi.SongStoreApi.Mapper;
using GitArApi.Common.Mongo;
using GitArApi.SongStoreApi.Services.Command.Abstractions;

namespace GitArApi.SongStoreApi.Services.Command;

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

    public async Task UpdateSongAsync(string id, UpdateSongRequest request, CancellationToken cancellationToken)
    {
        var existing = await _songStore.GetDocumentAsync(id, cancellationToken);
        if (request == null)
        {
            throw new InvalidDataException();
        }
        var mapper = new CreateSongRequestMapper();
        var song = mapper.AutoMapUpdateSongRequestToSongDocument(request);
        song.Id = id;
        await _songStore.UpdateAsync(song, cancellationToken);
    }

    public async Task DeleteSongAsync(string id, CancellationToken cancellationToken)
    {
        var exists = _songStore.GetDocumentAsync(id, cancellationToken);
        if (exists == null)
        {
            throw new Exception("Song doesn't exist");
        }
        await _songStore.DeleteAsync(id, cancellationToken);
    }
}