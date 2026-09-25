
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class GiftCardRepository : IGiftCardRepository
{
    private readonly ApplicationDbContext _db;

    public GiftCardRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GiftCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GiftCards
            .Include(x => x.Customer)
            .Include(x => x.IssuedOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GiftCard>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GiftCards
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.IssuedOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GiftCard giftCard, CancellationToken cancellationToken)
    {
        _db.GiftCards.Add(giftCard);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GiftCard giftCard, CancellationToken cancellationToken)
    {
        _db.GiftCards.Update(giftCard);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GiftCard giftCard, CancellationToken cancellationToken)
    {
        _db.GiftCards.Remove(giftCard);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRedemptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GiftCardRedemptions
            .Where(giftCardRedemption =>
                request.ChildIds.Contains(giftCardRedemption.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    giftCardRedemption =>
                        EF.Property<Guid?>(
                            giftCardRedemption,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRedemptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GiftCardRedemptions
            .Where(giftCardRedemption =>
                request.ChildIds.Contains(giftCardRedemption.Id) &&
                EF.Property<Guid?>(
                    giftCardRedemption,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    giftCardRedemption =>
                        EF.Property<Guid?>(
                            giftCardRedemption,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
