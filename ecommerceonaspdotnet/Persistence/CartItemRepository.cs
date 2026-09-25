
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CartItemRepository : ICartItemRepository
{
    private readonly ApplicationDbContext _db;

    public CartItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CartItems
            .Include(x => x.Cart)
            .Include(x => x.Variant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CartItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CartItems
            .AsNoTracking()
            .Include(x => x.Cart)
            .Include(x => x.Variant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CartItem cartItem, CancellationToken cancellationToken)
    {
        _db.CartItems.Add(cartItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CartItem cartItem, CancellationToken cancellationToken)
    {
        _db.CartItems.Update(cartItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CartItem cartItem, CancellationToken cancellationToken)
    {
        _db.CartItems.Remove(cartItem);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAppliedPromotionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Promotions
            .Where(promotion =>
                request.ChildIds.Contains(promotion.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    promotion =>
                        EF.Property<Guid?>(
                            promotion,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAppliedPromotionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Promotions
            .Where(promotion =>
                request.ChildIds.Contains(promotion.Id) &&
                EF.Property<Guid?>(
                    promotion,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    promotion =>
                        EF.Property<Guid?>(
                            promotion,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
