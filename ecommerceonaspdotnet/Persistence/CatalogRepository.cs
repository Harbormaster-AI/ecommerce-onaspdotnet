
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CatalogRepository : ICatalogRepository
{
    private readonly ApplicationDbContext _db;

    public CatalogRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Catalog?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Catalogs
            .Include(x => x.Channel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Catalog>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Catalogs
            .AsNoTracking()
            .Include(x => x.Channel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Catalog catalog, CancellationToken cancellationToken)
    {
        _db.Catalogs.Add(catalog);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Catalog catalog, CancellationToken cancellationToken)
    {
        _db.Catalogs.Update(catalog);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Catalog catalog, CancellationToken cancellationToken)
    {
        _db.Catalogs.Remove(catalog);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Categorys
            .Where(category =>
                request.ChildIds.Contains(category.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    category =>
                        EF.Property<Guid?>(
                            category,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Categorys
            .Where(category =>
                request.ChildIds.Contains(category.Id) &&
                EF.Property<Guid?>(
                    category,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    category =>
                        EF.Property<Guid?>(
                            category,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
