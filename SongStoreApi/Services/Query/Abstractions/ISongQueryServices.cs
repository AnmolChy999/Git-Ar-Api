namespace GitArApi.SongStoreApi.Services.Query.Abstractions;

using GitArApi.SongStoreApi.Documents;
public interface ISongQueryServices
{
    HashSet<Song> GetSongsAsync(string songName, CancellationToken cancellationToken);
}