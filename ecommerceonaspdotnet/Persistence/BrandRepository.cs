using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _db;

    public BrandRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Brands
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Brand>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Brands
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Brand brand, CancellationToken cancellationToken)
    {
        _db.Brands.Add(brand);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Brand brand, CancellationToken cancellationToken)
    {
        _db.Brands.Update(brand);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Brand brand, CancellationToken cancellationToken)
    {
        _db.Brands.Remove(brand);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
