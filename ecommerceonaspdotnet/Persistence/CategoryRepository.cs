
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Categorys
            .Include(x => x.Catalog)
            .Include(x => x.ParentCategory)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Categorys
            .AsNoTracking()
            .Include(x => x.Catalog)
            .Include(x => x.ParentCategory)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        _db.Categorys.Add(category);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        _db.Categorys.Update(category);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        _db.Categorys.Remove(category);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToSubcategoriesAsync(
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

    public async Task RemoveFromSubcategoriesAsync(
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

}
