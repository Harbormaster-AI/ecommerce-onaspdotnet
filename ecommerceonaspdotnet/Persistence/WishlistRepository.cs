
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _db;

    public WishlistRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Wishlist?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Wishlists
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Wishlist>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Wishlists
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Wishlist wishlist, CancellationToken cancellationToken)
    {
        _db.Wishlists.Add(wishlist);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Wishlist wishlist, CancellationToken cancellationToken)
    {
        _db.Wishlists.Update(wishlist);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Wishlist wishlist, CancellationToken cancellationToken)
    {
        _db.Wishlists.Remove(wishlist);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WishlistItems
            .Where(wishlistItem =>
                request.ChildIds.Contains(wishlistItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wishlistItem =>
                        EF.Property<Guid?>(
                            wishlistItem,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WishlistItems
            .Where(wishlistItem =>
                request.ChildIds.Contains(wishlistItem.Id) &&
                EF.Property<Guid?>(
                    wishlistItem,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wishlistItem =>
                        EF.Property<Guid?>(
                            wishlistItem,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
