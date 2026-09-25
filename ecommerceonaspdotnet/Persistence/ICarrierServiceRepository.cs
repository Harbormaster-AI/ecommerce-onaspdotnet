using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICarrierServiceRepository
{
    Task<CarrierService?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarrierService>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CarrierService carrierService, CancellationToken cancellationToken);
    Task UpdateAsync(CarrierService carrierService, CancellationToken cancellationToken);
    Task DeleteAsync(CarrierService carrierService, CancellationToken cancellationToken);

    Task AddToShippingMethodsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShippingMethodsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
