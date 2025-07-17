using SongStoreApi.Documents;

namespace SongStoreApi.Services.Abstractions;
public interface ISongServices
{
    HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken);
}