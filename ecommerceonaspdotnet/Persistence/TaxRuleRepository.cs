
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class TaxRuleRepository : ITaxRuleRepository
{
    private readonly ApplicationDbContext _db;

    public TaxRuleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TaxRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TaxRules
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TaxRule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TaxRules
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TaxRule taxRule, CancellationToken cancellationToken)
    {
        _db.TaxRules.Add(taxRule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaxRule taxRule, CancellationToken cancellationToken)
    {
        _db.TaxRules.Update(taxRule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaxRule taxRule, CancellationToken cancellationToken)
    {
        _db.TaxRules.Remove(taxRule);
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
