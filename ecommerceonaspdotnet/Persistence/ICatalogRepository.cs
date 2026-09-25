using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICatalogRepository
{
    Task<Catalog?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Catalog>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Catalog catalog, CancellationToken cancellationToken);
    Task UpdateAsync(Catalog catalog, CancellationToken cancellationToken);
    Task DeleteAsync(Catalog catalog, CancellationToken cancellationToken);

    Task AddToCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
