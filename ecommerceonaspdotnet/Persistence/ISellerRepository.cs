using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ISellerRepository
{
    Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Seller>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Seller seller, CancellationToken cancellationToken);
    Task UpdateAsync(Seller seller, CancellationToken cancellationToken);
    Task DeleteAsync(Seller seller, CancellationToken cancellationToken);

    Task AddToProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
