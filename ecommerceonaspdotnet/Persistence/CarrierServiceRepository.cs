using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CarrierServiceRepository : ICarrierServiceRepository
{
    private readonly ApplicationDbContext _db;

    public CarrierServiceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CarrierService?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CarrierServices
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CarrierService>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CarrierServices
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CarrierService carrierService, CancellationToken cancellationToken)
    {
        _db.CarrierServices.Add(carrierService);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CarrierService carrierService, CancellationToken cancellationToken)
    {
        _db.CarrierServices.Update(carrierService);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CarrierService carrierService, CancellationToken cancellationToken)
    {
        _db.CarrierServices.Remove(carrierService);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
