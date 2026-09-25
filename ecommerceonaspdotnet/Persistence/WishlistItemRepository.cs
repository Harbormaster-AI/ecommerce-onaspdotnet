
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class WishlistItemRepository : IWishlistItemRepository
{
    private readonly ApplicationDbContext _db;

    public WishlistItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WishlistItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WishlistItems
            .Include(x => x.Wishlist)
            .Include(x => x.Variant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WishlistItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WishlistItems
            .AsNoTracking()
            .Include(x => x.Wishlist)
            .Include(x => x.Variant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WishlistItem wishlistItem, CancellationToken cancellationToken)
    {
        _db.WishlistItems.Add(wishlistItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WishlistItem wishlistItem, CancellationToken cancellationToken)
    {
        _db.WishlistItems.Update(wishlistItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WishlistItem wishlistItem, CancellationToken cancellationToken)
    {
        _db.WishlistItems.Remove(wishlistItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
