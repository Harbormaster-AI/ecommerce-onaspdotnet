
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;

    public CartRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Carts
            .Include(x => x.Customer)
            .Include(x => x.Channel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Cart>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Carts
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Channel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Cart cart, CancellationToken cancellationToken)
    {
        _db.Carts.Add(cart);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken)
    {
        _db.Carts.Update(cart);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken)
    {
        _db.Carts.Remove(cart);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CartItems
            .Where(cartItem =>
                request.ChildIds.Contains(cartItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cartItem =>
                        EF.Property<Guid?>(
                            cartItem,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CartItems
            .Where(cartItem =>
                request.ChildIds.Contains(cartItem.Id) &&
                EF.Property<Guid?>(
                    cartItem,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cartItem =>
                        EF.Property<Guid?>(
                            cartItem,
                            "Payout_Id"),
                    (Guid?)null));
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
