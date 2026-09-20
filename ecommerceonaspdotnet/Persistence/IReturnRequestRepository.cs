using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IReturnRequestRepository
{
    Task<ReturnRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ReturnRequest>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ReturnRequest returnRequest, CancellationToken cancellationToken);
    Task UpdateAsync(ReturnRequest returnRequest, CancellationToken cancellationToken);
    Task DeleteAsync(ReturnRequest returnRequest, CancellationToken cancellationToken);
}
