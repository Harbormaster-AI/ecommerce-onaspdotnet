
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _db;

    public SupplierRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Add(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Update(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Remove(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProductsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Products
            .Where(product =>
                request.ChildIds.Contains(product.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    product =>
                        EF.Property<Guid?>(
                            product,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Products
            .Where(product =>
                request.ChildIds.Contains(product.Id) &&
                EF.Property<Guid?>(
                    product,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    product =>
                        EF.Property<Guid?>(
                            product,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToFulfillmentCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FulfillmentCenters
            .Where(fulfillmentCenter =>
                request.ChildIds.Contains(fulfillmentCenter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    fulfillmentCenter =>
                        EF.Property<Guid?>(
                            fulfillmentCenter,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFulfillmentCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FulfillmentCenters
            .Where(fulfillmentCenter =>
                request.ChildIds.Contains(fulfillmentCenter.Id) &&
                EF.Property<Guid?>(
                    fulfillmentCenter,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    fulfillmentCenter =>
                        EF.Property<Guid?>(
                            fulfillmentCenter,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
