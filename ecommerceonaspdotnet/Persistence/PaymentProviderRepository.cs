using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class PaymentProviderRepository : IPaymentProviderRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentProviderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentProviders
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentProvider>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentProviders
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken)
    {
        _db.PaymentProviders.Add(paymentProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken)
    {
        _db.PaymentProviders.Update(paymentProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentProvider paymentProvider, CancellationToken cancellationToken)
    {
        _db.PaymentProviders.Remove(paymentProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
