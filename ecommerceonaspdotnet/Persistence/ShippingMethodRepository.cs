using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ShippingMethodRepository : IShippingMethodRepository
{
    private readonly ApplicationDbContext _db;

    public ShippingMethodRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ShippingMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ShippingMethods
            .Include(x => x.CarrierService)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ShippingMethod>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ShippingMethods
            .AsNoTracking()
            .Include(x => x.CarrierService)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ShippingMethod shippingMethod, CancellationToken cancellationToken)
    {
        _db.ShippingMethods.Add(shippingMethod);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ShippingMethod shippingMethod, CancellationToken cancellationToken)
    {
        _db.ShippingMethods.Update(shippingMethod);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ShippingMethod shippingMethod, CancellationToken cancellationToken)
    {
        _db.ShippingMethods.Remove(shippingMethod);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
