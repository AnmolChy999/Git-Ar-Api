namespace GitArApi.SongStoreApi.Repository;

using MongoDB.Driver;
using GitArApi.SongStoreApi.Constants;

public interface IDocumentStore<T> where T : IDocument
{
    Task<T> GetDocumentAsync(string id, CancellationToken cancellationToken);
    Task<T> GetDocumentAsync(FilterDefinition<T> filter, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync();
    Task InsertAsync(T document, CancellationToken cancellationToken);
    Task UpsertAsync(T document, CancellationToken cancellationToken);
    Task UpdateAsync(T document, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}