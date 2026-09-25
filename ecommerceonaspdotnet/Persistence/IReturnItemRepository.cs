using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IReturnItemRepository
{
    Task<ReturnItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ReturnItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ReturnItem returnItem, CancellationToken cancellationToken);
    Task UpdateAsync(ReturnItem returnItem, CancellationToken cancellationToken);
    Task DeleteAsync(ReturnItem returnItem, CancellationToken cancellationToken);


}
