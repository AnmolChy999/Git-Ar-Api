using MongoDB.Driver;
using SongStoreApi.Constants;

namespace SongStoreApi.Repository;

public interface IDocumentStore<T> where T : IDocument
{
    Task InsertAsync(T document, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<T> GetDocumentAsync(FilterDefinition<T> filter, CancellationToken cancellationToken, bool includeDeleted = false);
    Task<List<T>> GetAllAsync();
    Task UpdateAsync(T document, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task UpsertAsync(T document, CancellationToken cancellationToken);
}