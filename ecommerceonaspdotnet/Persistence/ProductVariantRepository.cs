using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ProductVariantRepository : IProductVariantRepository
{
    private readonly ApplicationDbContext _db;

    public ProductVariantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductVariants
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductVariant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductVariants
            .AsNoTracking()
            .Include(x => x.Product)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken)
    {
        _db.ProductVariants.Add(productVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductVariant productVariant, CancellationToken cancellationToken)
    {
        _db.ProductVariants.Update(productVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductVariant productVariant, CancellationToken cancellationToken)
    {
        _db.ProductVariants.Remove(productVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
