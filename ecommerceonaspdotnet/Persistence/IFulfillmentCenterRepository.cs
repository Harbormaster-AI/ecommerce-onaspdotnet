using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IFulfillmentCenterRepository
{
    Task<FulfillmentCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FulfillmentCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken);
    Task UpdateAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken);
    Task DeleteAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken);
}
