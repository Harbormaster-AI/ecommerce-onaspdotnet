using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IShipmentItemRepository
{
    Task<ShipmentItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ShipmentItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ShipmentItem shipmentItem, CancellationToken cancellationToken);
    Task UpdateAsync(ShipmentItem shipmentItem, CancellationToken cancellationToken);
    Task DeleteAsync(ShipmentItem shipmentItem, CancellationToken cancellationToken);


}
