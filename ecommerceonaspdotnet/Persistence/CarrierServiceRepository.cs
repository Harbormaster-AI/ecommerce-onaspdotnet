
using ecommerceonaspdotnet.Contracts;
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


    public async Task AddToShippingMethodsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShippingMethods
            .Where(shippingMethod =>
                request.ChildIds.Contains(shippingMethod.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shippingMethod =>
                        EF.Property<Guid?>(
                            shippingMethod,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromShippingMethodsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShippingMethods
            .Where(shippingMethod =>
                request.ChildIds.Contains(shippingMethod.Id) &&
                EF.Property<Guid?>(
                    shippingMethod,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shippingMethod =>
                        EF.Property<Guid?>(
                            shippingMethod,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
