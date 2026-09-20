using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface ISellerRepository
{
    Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Seller>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Seller seller, CancellationToken cancellationToken);
    Task UpdateAsync(Seller seller, CancellationToken cancellationToken);
    Task DeleteAsync(Seller seller, CancellationToken cancellationToken);
}
