using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IShipmentRepository
{
    Task<Shipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Shipment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Shipment shipment, CancellationToken cancellationToken);
    Task UpdateAsync(Shipment shipment, CancellationToken cancellationToken);
    Task DeleteAsync(Shipment shipment, CancellationToken cancellationToken);

    Task AddToShipmentItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShipmentItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
