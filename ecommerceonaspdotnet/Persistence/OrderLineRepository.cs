
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class OrderLineRepository : IOrderLineRepository
{
    private readonly ApplicationDbContext _db;

    public OrderLineRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<OrderLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.OrderLines
            .Include(x => x.Order)
            .Include(x => x.Variant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<OrderLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.OrderLines
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Variant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(OrderLine orderLine, CancellationToken cancellationToken)
    {
        _db.OrderLines.Add(orderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(OrderLine orderLine, CancellationToken cancellationToken)
    {
        _db.OrderLines.Update(orderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(OrderLine orderLine, CancellationToken cancellationToken)
    {
        _db.OrderLines.Remove(orderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAppliedPromotionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Promotions
            .Where(promotion =>
                request.ChildIds.Contains(promotion.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    promotion =>
                        EF.Property<Guid?>(
                            promotion,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAppliedPromotionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Promotions
            .Where(promotion =>
                request.ChildIds.Contains(promotion.Id) &&
                EF.Property<Guid?>(
                    promotion,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    promotion =>
                        EF.Property<Guid?>(
                            promotion,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
