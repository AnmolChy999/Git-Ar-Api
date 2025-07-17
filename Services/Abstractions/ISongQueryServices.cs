using SongStoreApi.Documents;

namespace SongStoreApi.Services.Abstractions;
public interface ISongQueryServices
{
    HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken);
}