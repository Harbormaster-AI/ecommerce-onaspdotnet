
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class RefundRepository : IRefundRepository
{
    private readonly ApplicationDbContext _db;

    public RefundRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Refund?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Refunds
            .Include(x => x.Payment)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Refund>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Refunds
            .AsNoTracking()
            .Include(x => x.Payment)
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Refund refund, CancellationToken cancellationToken)
    {
        _db.Refunds.Add(refund);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Refund refund, CancellationToken cancellationToken)
    {
        _db.Refunds.Update(refund);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Refund refund, CancellationToken cancellationToken)
    {
        _db.Refunds.Remove(refund);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
