using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Category category, CancellationToken cancellationToken);
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    Task DeleteAsync(Category category, CancellationToken cancellationToken);

    Task AddToSubcategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSubcategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
