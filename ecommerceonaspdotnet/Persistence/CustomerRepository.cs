
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _db;

    public CustomerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Customers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAddressesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CustomerAddresss
            .Where(customerAddress =>
                request.ChildIds.Contains(customerAddress.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customerAddress =>
                        EF.Property<Guid?>(
                            customerAddress,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAddressesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CustomerAddresss
            .Where(customerAddress =>
                request.ChildIds.Contains(customerAddress.Id) &&
                EF.Property<Guid?>(
                    customerAddress,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customerAddress =>
                        EF.Property<Guid?>(
                            customerAddress,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToCartsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Carts
            .Where(cart =>
                request.ChildIds.Contains(cart.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cart =>
                        EF.Property<Guid?>(
                            cart,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCartsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Carts
            .Where(cart =>
                request.ChildIds.Contains(cart.Id) &&
                EF.Property<Guid?>(
                    cart,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cart =>
                        EF.Property<Guid?>(
                            cart,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Orders
            .Where(order =>
                request.ChildIds.Contains(order.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    order =>
                        EF.Property<Guid?>(
                            order,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Orders
            .Where(order =>
                request.ChildIds.Contains(order.Id) &&
                EF.Property<Guid?>(
                    order,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    order =>
                        EF.Property<Guid?>(
                            order,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payments
            .Where(payment =>
                request.ChildIds.Contains(payment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payment =>
                        EF.Property<Guid?>(
                            payment,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payments
            .Where(payment =>
                request.ChildIds.Contains(payment.Id) &&
                EF.Property<Guid?>(
                    payment,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payment =>
                        EF.Property<Guid?>(
                            payment,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Reviews
            .Where(review =>
                request.ChildIds.Contains(review.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    review =>
                        EF.Property<Guid?>(
                            review,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Reviews
            .Where(review =>
                request.ChildIds.Contains(review.Id) &&
                EF.Property<Guid?>(
                    review,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    review =>
                        EF.Property<Guid?>(
                            review,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToWishlistsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Wishlists
            .Where(wishlist =>
                request.ChildIds.Contains(wishlist.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wishlist =>
                        EF.Property<Guid?>(
                            wishlist,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWishlistsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Wishlists
            .Where(wishlist =>
                request.ChildIds.Contains(wishlist.Id) &&
                EF.Property<Guid?>(
                    wishlist,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wishlist =>
                        EF.Property<Guid?>(
                            wishlist,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToSubscriptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Subscriptions
            .Where(subscription =>
                request.ChildIds.Contains(subscription.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    subscription =>
                        EF.Property<Guid?>(
                            subscription,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSubscriptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Subscriptions
            .Where(subscription =>
                request.ChildIds.Contains(subscription.Id) &&
                EF.Property<Guid?>(
                    subscription,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    subscription =>
                        EF.Property<Guid?>(
                            subscription,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToCouponRedemptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CouponRedemptions
            .Where(couponRedemption =>
                request.ChildIds.Contains(couponRedemption.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    couponRedemption =>
                        EF.Property<Guid?>(
                            couponRedemption,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCouponRedemptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CouponRedemptions
            .Where(couponRedemption =>
                request.ChildIds.Contains(couponRedemption.Id) &&
                EF.Property<Guid?>(
                    couponRedemption,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    couponRedemption =>
                        EF.Property<Guid?>(
                            couponRedemption,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToGiftCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GiftCards
            .Where(giftCard =>
                request.ChildIds.Contains(giftCard.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    giftCard =>
                        EF.Property<Guid?>(
                            giftCard,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromGiftCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GiftCards
            .Where(giftCard =>
                request.ChildIds.Contains(giftCard.Id) &&
                EF.Property<Guid?>(
                    giftCard,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    giftCard =>
                        EF.Property<Guid?>(
                            giftCard,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
