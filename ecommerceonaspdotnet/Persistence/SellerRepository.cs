using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class SellerRepository : ISellerRepository
{
    private readonly ApplicationDbContext _db;

    public SellerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Sellers
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Seller>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Sellers
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Seller seller, CancellationToken cancellationToken)
    {
        _db.Sellers.Add(seller);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Seller seller, CancellationToken cancellationToken)
    {
        _db.Sellers.Update(seller);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Seller seller, CancellationToken cancellationToken)
    {
        _db.Sellers.Remove(seller);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
