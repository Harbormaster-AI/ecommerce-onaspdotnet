using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class FulfillmentCenterRepository : IFulfillmentCenterRepository
{
    private readonly ApplicationDbContext _db;

    public FulfillmentCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FulfillmentCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FulfillmentCenters
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FulfillmentCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FulfillmentCenters
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken)
    {
        _db.FulfillmentCenters.Add(fulfillmentCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken)
    {
        _db.FulfillmentCenters.Update(fulfillmentCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken)
    {
        _db.FulfillmentCenters.Remove(fulfillmentCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
