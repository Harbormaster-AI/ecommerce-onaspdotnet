
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IFulfillmentCenterService {

    Task Create(FulfillmentCenter model , CancellationToken cancellationToken);
    Task<bool> Update(FulfillmentCenter model, CancellationToken cancellationToken);
    Task<FulfillmentCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FulfillmentCenter>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToShipments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromShipments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class FulfillmentCenterService : IFulfillmentCenterService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFulfillmentCenterRepository _repository;
    private readonly ILogger<FulfillmentCenterService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FulfillmentCenterService(
        ApplicationTelemetry telemetry,
        IFulfillmentCenterRepository repository,
        ILogger<FulfillmentCenterService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(FulfillmentCenter model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "FulfillmentCenter",
                "CreateFulfillmentCenter",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(FulfillmentCenter model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.CenterCode = model.CenterCode;
            existing.Address = model.Address;
            existing.Timezone = model.Timezone;
            existing.AsActive = model.AsActive;

            await _telemetry.Execute(
                "FulfillmentCenter",
                "UpdateFulfillmentCenter",
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

    public Task<FulfillmentCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FulfillmentCenter>> GetAll(CancellationToken cancellationToken)
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
                "FulfillmentCenter",
                "UpdateFulfillmentCenter",
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

    public async Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FulfillmentCenter found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<MerchantService>().Get(childRequest, cancellationToken);
            parent.Merchant = child;
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

    public async Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FulfillmentCenter found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Merchant = null;
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


    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FulfillmentCenter",
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
                "FulfillmentCenter",
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

    public async Task<bool> AddToShipments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FulfillmentCenter",
                "AddToShipments",
                () => _repository.AddToShipmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromShipments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FulfillmentCenter",
                "RemoveFromShipments",
                () => _repository.RemoveFromShipmentsAsync(request, cancellationToken));
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
