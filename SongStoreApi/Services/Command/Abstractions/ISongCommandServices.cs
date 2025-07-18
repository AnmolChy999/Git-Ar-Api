namespace SongStoreApi.Services.Command.Abstractions;

public interface ISongCommandServices
{
    Task AddSongAsync(CreateSongRequest request, CancellationToken cancellationToken);
}