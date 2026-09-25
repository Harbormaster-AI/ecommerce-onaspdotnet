
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ReturnRequestRepository : IReturnRequestRepository
{
    private readonly ApplicationDbContext _db;

    public ReturnRequestRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ReturnRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ReturnRequests
            .Include(x => x.Order)
            .Include(x => x.Refund)
            .Include(x => x.Shipment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ReturnRequest>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ReturnRequests
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Refund)
            .Include(x => x.Shipment)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ReturnRequest returnRequest, CancellationToken cancellationToken)
    {
        _db.ReturnRequests.Add(returnRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ReturnRequest returnRequest, CancellationToken cancellationToken)
    {
        _db.ReturnRequests.Update(returnRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReturnRequest returnRequest, CancellationToken cancellationToken)
    {
        _db.ReturnRequests.Remove(returnRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ReturnItems
            .Where(returnItem =>
                request.ChildIds.Contains(returnItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    returnItem =>
                        EF.Property<Guid?>(
                            returnItem,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ReturnItems
            .Where(returnItem =>
                request.ChildIds.Contains(returnItem.Id) &&
                EF.Property<Guid?>(
                    returnItem,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    returnItem =>
                        EF.Property<Guid?>(
                            returnItem,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
