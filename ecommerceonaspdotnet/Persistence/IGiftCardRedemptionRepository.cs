using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IGiftCardRedemptionRepository
{
    Task<GiftCardRedemption?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GiftCardRedemption>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GiftCardRedemption giftCardRedemption, CancellationToken cancellationToken);
    Task UpdateAsync(GiftCardRedemption giftCardRedemption, CancellationToken cancellationToken);
    Task DeleteAsync(GiftCardRedemption giftCardRedemption, CancellationToken cancellationToken);


}
