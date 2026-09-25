
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CouponRedemptionRepository : ICouponRedemptionRepository
{
    private readonly ApplicationDbContext _db;

    public CouponRedemptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CouponRedemption?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CouponRedemptions
            .Include(x => x.Coupon)
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CouponRedemption>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CouponRedemptions
            .AsNoTracking()
            .Include(x => x.Coupon)
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CouponRedemption couponRedemption, CancellationToken cancellationToken)
    {
        _db.CouponRedemptions.Add(couponRedemption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CouponRedemption couponRedemption, CancellationToken cancellationToken)
    {
        _db.CouponRedemptions.Update(couponRedemption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CouponRedemption couponRedemption, CancellationToken cancellationToken)
    {
        _db.CouponRedemptions.Remove(couponRedemption);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
