using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Payments
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .Include(x => x.PaymentProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Payments
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .Include(x => x.PaymentProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
    {
        _db.Payments.Add(payment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken)
    {
        _db.Payments.Update(payment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Payment payment, CancellationToken cancellationToken)
    {
        _db.Payments.Remove(payment);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
