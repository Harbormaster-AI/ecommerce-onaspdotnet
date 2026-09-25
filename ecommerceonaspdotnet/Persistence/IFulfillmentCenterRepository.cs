using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IFulfillmentCenterRepository
{
    Task<FulfillmentCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FulfillmentCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken);
    Task UpdateAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken);
    Task DeleteAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken);

    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToShipmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShipmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
