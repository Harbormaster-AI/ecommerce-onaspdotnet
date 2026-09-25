
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .Include(x => x.Customer)
            .Include(x => x.Channel)
            .Include(x => x.Seller)
            .Include(x => x.Invoice)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Orders
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Channel)
            .Include(x => x.Seller)
            .Include(x => x.Invoice)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        _db.Orders.Add(order);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        _db.Orders.Update(order);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Order order, CancellationToken cancellationToken)
    {
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToOrderLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.OrderLines
            .Where(orderLine =>
                request.ChildIds.Contains(orderLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    orderLine =>
                        EF.Property<Guid?>(
                            orderLine,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOrderLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.OrderLines
            .Where(orderLine =>
                request.ChildIds.Contains(orderLine.Id) &&
                EF.Property<Guid?>(
                    orderLine,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    orderLine =>
                        EF.Property<Guid?>(
                            orderLine,
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


    public async Task AddToShipmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Shipments
            .Where(shipment =>
                request.ChildIds.Contains(shipment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shipment =>
                        EF.Property<Guid?>(
                            shipment,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromShipmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Shipments
            .Where(shipment =>
                request.ChildIds.Contains(shipment.Id) &&
                EF.Property<Guid?>(
                    shipment,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shipment =>
                        EF.Property<Guid?>(
                            shipment,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToRefundsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Refunds
            .Where(refund =>
                request.ChildIds.Contains(refund.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    refund =>
                        EF.Property<Guid?>(
                            refund,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRefundsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Refunds
            .Where(refund =>
                request.ChildIds.Contains(refund.Id) &&
                EF.Property<Guid?>(
                    refund,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    refund =>
                        EF.Property<Guid?>(
                            refund,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToAppliedPromotionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Promotions
            .Where(promotion =>
                request.ChildIds.Contains(promotion.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    promotion =>
                        EF.Property<Guid?>(
                            promotion,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAppliedPromotionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Promotions
            .Where(promotion =>
                request.ChildIds.Contains(promotion.Id) &&
                EF.Property<Guid?>(
                    promotion,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    promotion =>
                        EF.Property<Guid?>(
                            promotion,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToGiftCardRedemptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GiftCardRedemptions
            .Where(giftCardRedemption =>
                request.ChildIds.Contains(giftCardRedemption.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    giftCardRedemption =>
                        EF.Property<Guid?>(
                            giftCardRedemption,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromGiftCardRedemptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GiftCardRedemptions
            .Where(giftCardRedemption =>
                request.ChildIds.Contains(giftCardRedemption.Id) &&
                EF.Property<Guid?>(
                    giftCardRedemption,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    giftCardRedemption =>
                        EF.Property<Guid?>(
                            giftCardRedemption,
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


    public async Task AddToReturnRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ReturnRequests
            .Where(returnRequest =>
                request.ChildIds.Contains(returnRequest.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    returnRequest =>
                        EF.Property<Guid?>(
                            returnRequest,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromReturnRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ReturnRequests
            .Where(returnRequest =>
                request.ChildIds.Contains(returnRequest.Id) &&
                EF.Property<Guid?>(
                    returnRequest,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    returnRequest =>
                        EF.Property<Guid?>(
                            returnRequest,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
