
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ReturnItemRepository : IReturnItemRepository
{
    private readonly ApplicationDbContext _db;

    public ReturnItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ReturnItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ReturnItems
            .Include(x => x.ReturnRequest)
            .Include(x => x.OrderLine)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ReturnItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ReturnItems
            .AsNoTracking()
            .Include(x => x.ReturnRequest)
            .Include(x => x.OrderLine)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ReturnItem returnItem, CancellationToken cancellationToken)
    {
        _db.ReturnItems.Add(returnItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ReturnItem returnItem, CancellationToken cancellationToken)
    {
        _db.ReturnItems.Update(returnItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReturnItem returnItem, CancellationToken cancellationToken)
    {
        _db.ReturnItems.Remove(returnItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
