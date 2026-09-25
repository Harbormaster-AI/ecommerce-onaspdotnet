
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IInventoryItemService {

    Task Create(InventoryItem model , CancellationToken cancellationToken);
    Task<bool> Update(InventoryItem model, CancellationToken cancellationToken);
    Task<InventoryItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InventoryItem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken);


}

public class InventoryItemService : IInventoryItemService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInventoryItemRepository _repository;
    private readonly ILogger<InventoryItemService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InventoryItemService(
        ApplicationTelemetry telemetry,
        IInventoryItemRepository repository,
        ILogger<InventoryItemService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InventoryItem model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InventoryItem",
                "CreateInventoryItem",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(InventoryItem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.QuantityOnHand = model.QuantityOnHand;
            existing.QuantityReserved = model.QuantityReserved;
            existing.SafetyStock = model.SafetyStock;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "InventoryItem",
                "UpdateInventoryItem",
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

    public Task<InventoryItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InventoryItem>> GetAll(CancellationToken cancellationToken)
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
                "InventoryItem",
                "UpdateInventoryItem",
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

    public async Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InventoryItem found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ProductVariantService>().Get(childRequest, cancellationToken);
            parent.Variant = child;
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

    public async Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InventoryItem found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Variant = null;
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

    public async Task<bool> AssignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InventoryItem found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<FulfillmentCenterService>().Get(childRequest, cancellationToken);
            parent.FulfillmentCenter = child;
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

    public async Task<bool> UnassignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InventoryItem found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.FulfillmentCenter = null;
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




}
