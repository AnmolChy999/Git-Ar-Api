namespace GitArApi.SongStoreApi.Services.Command.Abstractions;

using GitArApi.SongStoreApi.Contracts;

public interface ISongCommandServices
{
    Task AddSongAsync(CreateSongRequest request, CancellationToken cancellationToken);

    Task UpdateSongAsync(string id, UpdateSongRequest request, CancellationToken cancellationToken);
}