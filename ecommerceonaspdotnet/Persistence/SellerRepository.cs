
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class SellerRepository : ISellerRepository
{
    private readonly ApplicationDbContext _db;

    public SellerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Sellers
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Seller>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Sellers
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Seller seller, CancellationToken cancellationToken)
    {
        _db.Sellers.Add(seller);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Seller seller, CancellationToken cancellationToken)
    {
        _db.Sellers.Update(seller);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Seller seller, CancellationToken cancellationToken)
    {
        _db.Sellers.Remove(seller);
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


    public async Task AddToPayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payouts
            .Where(payout =>
                request.ChildIds.Contains(payout.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payout =>
                        EF.Property<Guid?>(
                            payout,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payouts
            .Where(payout =>
                request.ChildIds.Contains(payout.Id) &&
                EF.Property<Guid?>(
                    payout,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payout =>
                        EF.Property<Guid?>(
                            payout,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Orders
            .Where(order =>
                request.ChildIds.Contains(order.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    order =>
                        EF.Property<Guid?>(
                            order,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Orders
            .Where(order =>
                request.ChildIds.Contains(order.Id) &&
                EF.Property<Guid?>(
                    order,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    order =>
                        EF.Property<Guid?>(
                            order,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
