
using ecommerceonaspdotnet.Contracts;
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


    public async Task AddToChannelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Channels
            .Where(channel =>
                request.ChildIds.Contains(channel.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    channel =>
                        EF.Property<Guid?>(
                            channel,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromChannelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Channels
            .Where(channel =>
                request.ChildIds.Contains(channel.Id) &&
                EF.Property<Guid?>(
                    channel,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    channel =>
                        EF.Property<Guid?>(
                            channel,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
