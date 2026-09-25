using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IPaymentProviderRepository
{
    Task<PaymentProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentProvider>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken);

    Task AddToChannelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromChannelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSubscriptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSubscriptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
