
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IProductVariantRepository _repository;
    private readonly ILogger<ProductVariantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ProductVariantService(
        ApplicationTelemetry telemetry,
        IProductVariantRepository repository,
        ILogger<ProductVariantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ProductVariant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ProductVariant",
                "CreateProductVariant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "ProductVariant",
                "UpdateProductVariant",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "ProductVariant",
                "UpdateProductVariant",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignProduct(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ProductVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ProductService>().Get(childRequest, cancellationToken);
            parent.Product = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignProduct(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ProductVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Product = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToPricing(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToPricing",
                () => _repository.AddToPricingAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromPricing(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromPricing",
                () => _repository.RemoveFromPricingAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToInventoryItems",
                () => _repository.AddToInventoryItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromInventoryItems",
                () => _repository.RemoveFromInventoryItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToMediaAssets",
                () => _repository.AddToMediaAssetsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromMediaAssets",
                () => _repository.RemoveFromMediaAssetsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToSubscriptions",
                () => _repository.AddToSubscriptionsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromSubscriptions",
                () => _repository.RemoveFromSubscriptionsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToCartItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToCartItems",
                () => _repository.AddToCartItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromCartItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromCartItems",
                () => _repository.RemoveFromCartItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToOrderLines",
                () => _repository.AddToOrderLinesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromOrderLines",
                () => _repository.RemoveFromOrderLinesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToWishlistItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "AddToWishlistItems",
                () => _repository.AddToWishlistItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromWishlistItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductVariant",
                "RemoveFromWishlistItems",
                () => _repository.RemoveFromWishlistItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
