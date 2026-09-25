using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Supplier supplier, CancellationToken cancellationToken);
    Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken);

    Task AddToProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFulfillmentCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFulfillmentCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
