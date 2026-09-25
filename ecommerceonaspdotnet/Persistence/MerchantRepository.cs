
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class MerchantRepository : IMerchantRepository
{
    private readonly ApplicationDbContext _db;

    public MerchantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Merchants
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Merchants
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Add(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Update(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Remove(merchant);
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


    public async Task AddToBrandsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Brands
            .Where(brand =>
                request.ChildIds.Contains(brand.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    brand =>
                        EF.Property<Guid?>(
                            brand,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBrandsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Brands
            .Where(brand =>
                request.ChildIds.Contains(brand.Id) &&
                EF.Property<Guid?>(
                    brand,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    brand =>
                        EF.Property<Guid?>(
                            brand,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToFulfillmentCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FulfillmentCenters
            .Where(fulfillmentCenter =>
                request.ChildIds.Contains(fulfillmentCenter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    fulfillmentCenter =>
                        EF.Property<Guid?>(
                            fulfillmentCenter,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFulfillmentCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FulfillmentCenters
            .Where(fulfillmentCenter =>
                request.ChildIds.Contains(fulfillmentCenter.Id) &&
                EF.Property<Guid?>(
                    fulfillmentCenter,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    fulfillmentCenter =>
                        EF.Property<Guid?>(
                            fulfillmentCenter,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToTaxRulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TaxRules
            .Where(taxRule =>
                request.ChildIds.Contains(taxRule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    taxRule =>
                        EF.Property<Guid?>(
                            taxRule,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTaxRulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TaxRules
            .Where(taxRule =>
                request.ChildIds.Contains(taxRule.Id) &&
                EF.Property<Guid?>(
                    taxRule,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    taxRule =>
                        EF.Property<Guid?>(
                            taxRule,
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


    public async Task AddToSellersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Sellers
            .Where(seller =>
                request.ChildIds.Contains(seller.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    seller =>
                        EF.Property<Guid?>(
                            seller,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSellersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Sellers
            .Where(seller =>
                request.ChildIds.Contains(seller.Id) &&
                EF.Property<Guid?>(
                    seller,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    seller =>
                        EF.Property<Guid?>(
                            seller,
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

}
