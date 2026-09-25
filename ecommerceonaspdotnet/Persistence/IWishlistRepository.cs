using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IWishlistRepository
{
    Task<Wishlist?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Wishlist>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Wishlist wishlist, CancellationToken cancellationToken);
    Task UpdateAsync(Wishlist wishlist, CancellationToken cancellationToken);
    Task DeleteAsync(Wishlist wishlist, CancellationToken cancellationToken);

    Task AddToItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
