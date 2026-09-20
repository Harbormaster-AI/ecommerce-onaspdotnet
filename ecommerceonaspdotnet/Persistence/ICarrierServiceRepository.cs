using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface ICarrierServiceRepository
{
    Task<CarrierService?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarrierService>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CarrierService carrierService, CancellationToken cancellationToken);
    Task UpdateAsync(CarrierService carrierService, CancellationToken cancellationToken);
    Task DeleteAsync(CarrierService carrierService, CancellationToken cancellationToken);
}
