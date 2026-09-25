using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Payment payment, CancellationToken cancellationToken);
    Task UpdateAsync(Payment payment, CancellationToken cancellationToken);
    Task DeleteAsync(Payment payment, CancellationToken cancellationToken);

    Task AddToRefundsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRefundsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
