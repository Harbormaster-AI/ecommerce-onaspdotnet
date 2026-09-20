using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IGiftCardRepository
{
    Task<GiftCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GiftCard>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GiftCard giftCard, CancellationToken cancellationToken);
    Task UpdateAsync(GiftCard giftCard, CancellationToken cancellationToken);
    Task DeleteAsync(GiftCard giftCard, CancellationToken cancellationToken);
}
