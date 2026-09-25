using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);

    Task AddToCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMediaAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMediaAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
