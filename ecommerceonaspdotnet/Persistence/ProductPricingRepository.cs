
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ProductPricingRepository : IProductPricingRepository
{
    private readonly ApplicationDbContext _db;

    public ProductPricingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductPricing?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductPricings
            .Include(x => x.Variant)
            .Include(x => x.Channel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductPricing>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductPricings
            .AsNoTracking()
            .Include(x => x.Variant)
            .Include(x => x.Channel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductPricing productPricing, CancellationToken cancellationToken)
    {
        _db.ProductPricings.Add(productPricing);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductPricing productPricing, CancellationToken cancellationToken)
    {
        _db.ProductPricings.Update(productPricing);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductPricing productPricing, CancellationToken cancellationToken)
    {
        _db.ProductPricings.Remove(productPricing);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
