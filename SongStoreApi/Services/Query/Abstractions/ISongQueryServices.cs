using SongStoreApi.Documents;

namespace SongStoreApi.Services.Query.Abstractions;
public interface ISongQueryServices
{
    HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken);
}