
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class FulfillmentCenterRepository : IFulfillmentCenterRepository
{
    private readonly ApplicationDbContext _db;

    public FulfillmentCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FulfillmentCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FulfillmentCenters
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FulfillmentCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FulfillmentCenters
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken)
    {
        _db.FulfillmentCenters.Add(fulfillmentCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken)
    {
        _db.FulfillmentCenters.Update(fulfillmentCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FulfillmentCenter fulfillmentCenter, CancellationToken cancellationToken)
    {
        _db.FulfillmentCenters.Remove(fulfillmentCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToInventoryItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventoryItems
            .Where(inventoryItem =>
                request.ChildIds.Contains(inventoryItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventoryItem =>
                        EF.Property<Guid?>(
                            inventoryItem,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInventoryItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventoryItems
            .Where(inventoryItem =>
                request.ChildIds.Contains(inventoryItem.Id) &&
                EF.Property<Guid?>(
                    inventoryItem,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventoryItem =>
                        EF.Property<Guid?>(
                            inventoryItem,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToShipmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Shipments
            .Where(shipment =>
                request.ChildIds.Contains(shipment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shipment =>
                        EF.Property<Guid?>(
                            shipment,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromShipmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Shipments
            .Where(shipment =>
                request.ChildIds.Contains(shipment.Id) &&
                EF.Property<Guid?>(
                    shipment,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shipment =>
                        EF.Property<Guid?>(
                            shipment,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
