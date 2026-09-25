
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ShipmentItemRepository : IShipmentItemRepository
{
    private readonly ApplicationDbContext _db;

    public ShipmentItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ShipmentItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ShipmentItems
            .Include(x => x.Shipment)
            .Include(x => x.OrderLine)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ShipmentItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ShipmentItems
            .AsNoTracking()
            .Include(x => x.Shipment)
            .Include(x => x.OrderLine)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ShipmentItem shipmentItem, CancellationToken cancellationToken)
    {
        _db.ShipmentItems.Add(shipmentItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ShipmentItem shipmentItem, CancellationToken cancellationToken)
    {
        _db.ShipmentItems.Update(shipmentItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ShipmentItem shipmentItem, CancellationToken cancellationToken)
    {
        _db.ShipmentItems.Remove(shipmentItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
