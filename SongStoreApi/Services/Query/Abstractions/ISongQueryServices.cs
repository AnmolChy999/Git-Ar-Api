using GitArApi.SongStoreApi.Documents;

namespace GitArApi.SongStoreApi.Services.Query.Abstractions;
public interface ISongQueryServices
{
    HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken);
}