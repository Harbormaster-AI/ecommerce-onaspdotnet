using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ShipmentRepository : IShipmentRepository
{
    private readonly ApplicationDbContext _db;

    public ShipmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Shipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Shipments
            .Include(x => x.Order)
            .Include(x => x.FulfillmentCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Shipment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Shipments
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.FulfillmentCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Shipment shipment, CancellationToken cancellationToken)
    {
        _db.Shipments.Add(shipment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Shipment shipment, CancellationToken cancellationToken)
    {
        _db.Shipments.Update(shipment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Shipment shipment, CancellationToken cancellationToken)
    {
        _db.Shipments.Remove(shipment);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
