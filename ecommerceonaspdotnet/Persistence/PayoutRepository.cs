
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class PayoutRepository : IPayoutRepository
{
    private readonly ApplicationDbContext _db;

    public PayoutRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Payout?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Payouts
            .Include(x => x.Seller)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Payout>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Payouts
            .AsNoTracking()
            .Include(x => x.Seller)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Payout payout, CancellationToken cancellationToken)
    {
        _db.Payouts.Add(payout);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Payout payout, CancellationToken cancellationToken)
    {
        _db.Payouts.Update(payout);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Payout payout, CancellationToken cancellationToken)
    {
        _db.Payouts.Remove(payout);
        await _db.SaveChangesAsync(cancellationToken);
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
