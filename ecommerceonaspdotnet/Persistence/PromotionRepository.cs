
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class PromotionRepository : IPromotionRepository
{
    private readonly ApplicationDbContext _db;

    public PromotionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Promotions
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Promotion>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Promotions
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        _db.Promotions.Add(promotion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        _db.Promotions.Update(promotion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        _db.Promotions.Remove(promotion);
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


    public async Task AddToApplicableProductsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Products
            .Where(product =>
                request.ChildIds.Contains(product.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    product =>
                        EF.Property<Guid?>(
                            product,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromApplicableProductsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Products
            .Where(product =>
                request.ChildIds.Contains(product.Id) &&
                EF.Property<Guid?>(
                    product,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    product =>
                        EF.Property<Guid?>(
                            product,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToApplicableCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Categorys
            .Where(category =>
                request.ChildIds.Contains(category.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    category =>
                        EF.Property<Guid?>(
                            category,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromApplicableCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Categorys
            .Where(category =>
                request.ChildIds.Contains(category.Id) &&
                EF.Property<Guid?>(
                    category,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    category =>
                        EF.Property<Guid?>(
                            category,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToCouponsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Coupons
            .Where(coupon =>
                request.ChildIds.Contains(coupon.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    coupon =>
                        EF.Property<Guid?>(
                            coupon,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCouponsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Coupons
            .Where(coupon =>
                request.ChildIds.Contains(coupon.Id) &&
                EF.Property<Guid?>(
                    coupon,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    coupon =>
                        EF.Property<Guid?>(
                            coupon,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
