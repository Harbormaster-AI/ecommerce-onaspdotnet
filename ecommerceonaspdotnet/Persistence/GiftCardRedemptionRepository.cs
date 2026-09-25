
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class GiftCardRedemptionRepository : IGiftCardRedemptionRepository
{
    private readonly ApplicationDbContext _db;

    public GiftCardRedemptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GiftCardRedemption?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GiftCardRedemptions
            .Include(x => x.GiftCard)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GiftCardRedemption>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GiftCardRedemptions
            .AsNoTracking()
            .Include(x => x.GiftCard)
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GiftCardRedemption giftCardRedemption, CancellationToken cancellationToken)
    {
        _db.GiftCardRedemptions.Add(giftCardRedemption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GiftCardRedemption giftCardRedemption, CancellationToken cancellationToken)
    {
        _db.GiftCardRedemptions.Update(giftCardRedemption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GiftCardRedemption giftCardRedemption, CancellationToken cancellationToken)
    {
        _db.GiftCardRedemptions.Remove(giftCardRedemption);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
