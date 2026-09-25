
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class PaymentProviderRepository : IPaymentProviderRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentProviderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentProviders
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentProvider>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentProviders
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken)
    {
        _db.PaymentProviders.Add(paymentProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken)
    {
        _db.PaymentProviders.Update(paymentProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken)
    {
        _db.PaymentProviders.Remove(paymentProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToChannelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Channels
            .Where(channel =>
                request.ChildIds.Contains(channel.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    channel =>
                        EF.Property<Guid?>(
                            channel,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromChannelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Channels
            .Where(channel =>
                request.ChildIds.Contains(channel.Id) &&
                EF.Property<Guid?>(
                    channel,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    channel =>
                        EF.Property<Guid?>(
                            channel,
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

}
