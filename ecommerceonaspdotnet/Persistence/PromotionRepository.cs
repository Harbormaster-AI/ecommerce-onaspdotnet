using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class PromotionRepository : IPromotionRepository
{
    private readonly ApplicationDbContext _db;

    public PromotionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Promotions
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Promotion>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Promotions
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        _db.Promotions.Add(promotion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        _db.Promotions.Update(promotion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        _db.Promotions.Remove(promotion);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
