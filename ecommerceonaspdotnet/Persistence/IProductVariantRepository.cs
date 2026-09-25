using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IProductVariantRepository
{
    Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductVariant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken);
    Task UpdateAsync(ProductVariant productVariant, CancellationToken cancellationToken);
    Task DeleteAsync(ProductVariant productVariant, CancellationToken cancellationToken);

    Task AddToPricingAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPricingAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMediaAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMediaAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSubscriptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSubscriptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCartItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCartItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrderLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrderLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWishlistItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWishlistItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
