
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Products
            .Include(x => x.Brand)
            .Include(x => x.Seller)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Products
            .AsNoTracking()
            .Include(x => x.Brand)
            .Include(x => x.Seller)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Remove(product);
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


    public async Task AddToVariantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductVariants
            .Where(productVariant =>
                request.ChildIds.Contains(productVariant.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productVariant =>
                        EF.Property<Guid?>(
                            productVariant,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromVariantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductVariants
            .Where(productVariant =>
                request.ChildIds.Contains(productVariant.Id) &&
                EF.Property<Guid?>(
                    productVariant,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productVariant =>
                        EF.Property<Guid?>(
                            productVariant,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToMediaAssetsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MediaAssets
            .Where(mediaAsset =>
                request.ChildIds.Contains(mediaAsset.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    mediaAsset =>
                        EF.Property<Guid?>(
                            mediaAsset,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMediaAssetsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MediaAssets
            .Where(mediaAsset =>
                request.ChildIds.Contains(mediaAsset.Id) &&
                EF.Property<Guid?>(
                    mediaAsset,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    mediaAsset =>
                        EF.Property<Guid?>(
                            mediaAsset,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Reviews
            .Where(review =>
                request.ChildIds.Contains(review.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    review =>
                        EF.Property<Guid?>(
                            review,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Reviews
            .Where(review =>
                request.ChildIds.Contains(review.Id) &&
                EF.Property<Guid?>(
                    review,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    review =>
                        EF.Property<Guid?>(
                            review,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
