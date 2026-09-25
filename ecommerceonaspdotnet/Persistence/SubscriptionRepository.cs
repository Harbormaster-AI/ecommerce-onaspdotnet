
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _db;

    public SubscriptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Subscriptions
            .Include(x => x.Customer)
            .Include(x => x.Variant)
            .Include(x => x.PaymentProvider)
            .Include(x => x.Channel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Subscription>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Subscriptions
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Variant)
            .Include(x => x.PaymentProvider)
            .Include(x => x.Channel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        _db.Subscriptions.Add(subscription);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        _db.Subscriptions.Update(subscription);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        _db.Subscriptions.Remove(subscription);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
