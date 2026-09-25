using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IChannelRepository
{
    Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Channel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Channel channel, CancellationToken cancellationToken);
    Task UpdateAsync(Channel channel, CancellationToken cancellationToken);
    Task DeleteAsync(Channel channel, CancellationToken cancellationToken);

    Task AddToCatalogsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCatalogsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToShippingMethodsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShippingMethodsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
