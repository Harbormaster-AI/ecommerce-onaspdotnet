using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IOrderLineRepository
{
    Task<OrderLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<OrderLine>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(OrderLine orderLine, CancellationToken cancellationToken);
    Task UpdateAsync(OrderLine orderLine, CancellationToken cancellationToken);
    Task DeleteAsync(OrderLine orderLine, CancellationToken cancellationToken);
}
