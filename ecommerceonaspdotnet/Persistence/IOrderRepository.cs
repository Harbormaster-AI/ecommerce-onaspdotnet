using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAsync(Order order, CancellationToken cancellationToken);

    Task AddToOrderLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrderLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToShipmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShipmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRefundsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRefundsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAppliedPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAppliedPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGiftCardRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGiftCardRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCouponRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCouponRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReturnRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReturnRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
