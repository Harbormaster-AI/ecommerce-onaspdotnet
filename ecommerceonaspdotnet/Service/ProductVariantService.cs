using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IProductVariantService {

    Task Create(ProductVariant model , CancellationToken cancellationToken);
    Task<bool> Update(ProductVariant model, CancellationToken cancellationToken);
    Task<ProductVariant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductVariant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProduct(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProduct(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPricing(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPricing(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCartItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCartItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWishlistItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWishlistItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ProductVariantService : IProductVariantService
{
    private readonly IProductVariantRepository _repository;
    private readonly ILogger<ProductVariantService> _logger;

    public ProductVariantService(
        IProductVariantRepository repository, ILogger<ProductVariantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ProductVariant model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(ProductVariant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Sku = model.Sku;
            existing.Barcode = model.Barcode;
            existing.Title = model.Title;
            existing.Weight = model.Weight;
            existing.RequiresShipping = model.RequiresShipping;
            existing.WeightUnit = model.WeightUnit;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ProductVariant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ProductVariant>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignProduct(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProduct(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPricing(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPricing(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCartItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCartItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWishlistItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWishlistItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
