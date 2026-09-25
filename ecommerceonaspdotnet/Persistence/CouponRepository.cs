
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _db;

    public CouponRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Coupons
            .Include(x => x.Promotion)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Coupon>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Coupons
            .AsNoTracking()
            .Include(x => x.Promotion)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        _db.Coupons.Add(coupon);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        _db.Coupons.Update(coupon);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        _db.Coupons.Remove(coupon);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRedemptionsAsync(
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

    public async Task RemoveFromRedemptionsAsync(
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

}
