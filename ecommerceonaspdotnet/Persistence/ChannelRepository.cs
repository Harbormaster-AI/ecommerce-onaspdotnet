
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ChannelRepository : IChannelRepository
{
    private readonly ApplicationDbContext _db;

    public ChannelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Channels
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Channel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Channels
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Channel channel, CancellationToken cancellationToken)
    {
        _db.Channels.Add(channel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Channel channel, CancellationToken cancellationToken)
    {
        _db.Channels.Update(channel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Channel channel, CancellationToken cancellationToken)
    {
        _db.Channels.Remove(channel);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCatalogsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Catalogs
            .Where(catalog =>
                request.ChildIds.Contains(catalog.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    catalog =>
                        EF.Property<Guid?>(
                            catalog,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCatalogsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Catalogs
            .Where(catalog =>
                request.ChildIds.Contains(catalog.Id) &&
                EF.Property<Guid?>(
                    catalog,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    catalog =>
                        EF.Property<Guid?>(
                            catalog,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToPromotionsAsync(
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

    public async Task RemoveFromPromotionsAsync(
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


    public async Task AddToShippingMethodsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShippingMethods
            .Where(shippingMethod =>
                request.ChildIds.Contains(shippingMethod.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shippingMethod =>
                        EF.Property<Guid?>(
                            shippingMethod,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromShippingMethodsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShippingMethods
            .Where(shippingMethod =>
                request.ChildIds.Contains(shippingMethod.Id) &&
                EF.Property<Guid?>(
                    shippingMethod,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shippingMethod =>
                        EF.Property<Guid?>(
                            shippingMethod,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToPaymentProvidersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentProviders
            .Where(paymentProvider =>
                request.ChildIds.Contains(paymentProvider.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentProvider =>
                        EF.Property<Guid?>(
                            paymentProvider,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentProvidersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentProviders
            .Where(paymentProvider =>
                request.ChildIds.Contains(paymentProvider.Id) &&
                EF.Property<Guid?>(
                    paymentProvider,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentProvider =>
                        EF.Property<Guid?>(
                            paymentProvider,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
