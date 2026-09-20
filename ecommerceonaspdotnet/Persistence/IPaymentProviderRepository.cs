using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IPaymentProviderRepository
{
    Task<PaymentProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentProvider>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken);
}
