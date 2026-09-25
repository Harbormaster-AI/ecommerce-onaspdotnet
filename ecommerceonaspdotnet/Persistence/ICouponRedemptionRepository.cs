using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICouponRedemptionRepository
{
    Task<CouponRedemption?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CouponRedemption>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CouponRedemption couponRedemption, CancellationToken cancellationToken);
    Task UpdateAsync(CouponRedemption couponRedemption, CancellationToken cancellationToken);
    Task DeleteAsync(CouponRedemption couponRedemption, CancellationToken cancellationToken);


}
