using GitArApi.SongStoreApi.Contracts;

namespace GitArApi.SongStoreApi.Services.Command.Abstractions;

public interface ISongCommandServices
{
    Task AddSongAsync(CreateSongRequest request, CancellationToken cancellationToken);

    Task UpdateSongAsync(string id, UpdateSongRequest request, CancellationToken cancellationToken);

    Task DeleteSongAsync(string id, CancellationToken cancellationToken);
}