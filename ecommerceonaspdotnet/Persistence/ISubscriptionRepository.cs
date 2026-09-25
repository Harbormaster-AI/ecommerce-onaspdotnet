using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Subscription>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Subscription subscription, CancellationToken cancellationToken);
    Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken);
    Task DeleteAsync(Subscription subscription, CancellationToken cancellationToken);


}
