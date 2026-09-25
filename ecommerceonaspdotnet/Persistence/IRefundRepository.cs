using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IRefundRepository
{
    Task<Refund?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Refund>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Refund refund, CancellationToken cancellationToken);
    Task UpdateAsync(Refund refund, CancellationToken cancellationToken);
    Task DeleteAsync(Refund refund, CancellationToken cancellationToken);


}
