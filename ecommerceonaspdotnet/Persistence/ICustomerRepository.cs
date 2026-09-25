using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);

    Task AddToAddressesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAddressesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCartsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCartsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWishlistsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWishlistsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSubscriptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSubscriptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCouponRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCouponRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGiftCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGiftCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
