using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IShippingMethodRepository
{
    Task<ShippingMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ShippingMethod>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ShippingMethod shippingMethod, CancellationToken cancellationToken);
    Task UpdateAsync(ShippingMethod shippingMethod, CancellationToken cancellationToken);
    Task DeleteAsync(ShippingMethod shippingMethod, CancellationToken cancellationToken);
}
