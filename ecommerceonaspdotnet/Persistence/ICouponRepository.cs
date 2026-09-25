using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICouponRepository
{
    Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Coupon>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Coupon coupon, CancellationToken cancellationToken);
    Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken);
    Task DeleteAsync(Coupon coupon, CancellationToken cancellationToken);

    Task AddToRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRedemptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
