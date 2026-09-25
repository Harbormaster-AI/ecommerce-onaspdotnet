
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ProductVariantRepository : IProductVariantRepository
{
    private readonly ApplicationDbContext _db;

    public ProductVariantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductVariants
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductVariant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductVariants
            .AsNoTracking()
            .Include(x => x.Product)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken)
    {
        _db.ProductVariants.Add(productVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductVariant productVariant, CancellationToken cancellationToken)
    {
        _db.ProductVariants.Update(productVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductVariant productVariant, CancellationToken cancellationToken)
    {
        _db.ProductVariants.Remove(productVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPricingAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductPricings
            .Where(productPricing =>
                request.ChildIds.Contains(productPricing.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productPricing =>
                        EF.Property<Guid?>(
                            productPricing,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPricingAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductPricings
            .Where(productPricing =>
                request.ChildIds.Contains(productPricing.Id) &&
                EF.Property<Guid?>(
                    productPricing,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productPricing =>
                        EF.Property<Guid?>(
                            productPricing,
                            "Payout_Id"),
                    (Guid?)null));
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


    public async Task AddToSubscriptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Subscriptions
            .Where(subscription =>
                request.ChildIds.Contains(subscription.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    subscription =>
                        EF.Property<Guid?>(
                            subscription,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSubscriptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Subscriptions
            .Where(subscription =>
                request.ChildIds.Contains(subscription.Id) &&
                EF.Property<Guid?>(
                    subscription,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    subscription =>
                        EF.Property<Guid?>(
                            subscription,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToCartItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CartItems
            .Where(cartItem =>
                request.ChildIds.Contains(cartItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cartItem =>
                        EF.Property<Guid?>(
                            cartItem,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCartItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CartItems
            .Where(cartItem =>
                request.ChildIds.Contains(cartItem.Id) &&
                EF.Property<Guid?>(
                    cartItem,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cartItem =>
                        EF.Property<Guid?>(
                            cartItem,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToOrderLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.OrderLines
            .Where(orderLine =>
                request.ChildIds.Contains(orderLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    orderLine =>
                        EF.Property<Guid?>(
                            orderLine,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOrderLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.OrderLines
            .Where(orderLine =>
                request.ChildIds.Contains(orderLine.Id) &&
                EF.Property<Guid?>(
                    orderLine,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    orderLine =>
                        EF.Property<Guid?>(
                            orderLine,
                            "Payout_Id"),
                    (Guid?)null));
    }


    public async Task AddToWishlistItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WishlistItems
            .Where(wishlistItem =>
                request.ChildIds.Contains(wishlistItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wishlistItem =>
                        EF.Property<Guid?>(
                            wishlistItem,
                            "Payout_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWishlistItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WishlistItems
            .Where(wishlistItem =>
                request.ChildIds.Contains(wishlistItem.Id) &&
                EF.Property<Guid?>(
                    wishlistItem,
                    "Payout_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wishlistItem =>
                        EF.Property<Guid?>(
                            wishlistItem,
                            "Payout_Id"),
                    (Guid?)null));
    }

}
