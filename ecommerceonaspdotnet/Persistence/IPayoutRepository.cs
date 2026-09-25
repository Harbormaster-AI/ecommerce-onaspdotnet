using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IPayoutRepository
{
    Task<Payout?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Payout>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Payout payout, CancellationToken cancellationToken);
    Task UpdateAsync(Payout payout, CancellationToken cancellationToken);
    Task DeleteAsync(Payout payout, CancellationToken cancellationToken);

    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
