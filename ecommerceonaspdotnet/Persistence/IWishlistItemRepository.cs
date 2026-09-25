using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IWishlistItemRepository
{
    Task<WishlistItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WishlistItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WishlistItem wishlistItem, CancellationToken cancellationToken);
    Task UpdateAsync(WishlistItem wishlistItem, CancellationToken cancellationToken);
    Task DeleteAsync(WishlistItem wishlistItem, CancellationToken cancellationToken);


}
