using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IMerchantRepository
{
    Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Merchant merchant, CancellationToken cancellationToken);
    Task UpdateAsync(Merchant merchant, CancellationToken cancellationToken);
    Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken);
}
