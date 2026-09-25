
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IShippingMethodService {

    Task Create(ShippingMethod model , CancellationToken cancellationToken);
    Task<bool> Update(ShippingMethod model, CancellationToken cancellationToken);
    Task<ShippingMethod?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ShippingMethod>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCarrierService(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCarrierService(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ShippingMethodService : IShippingMethodService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IShippingMethodRepository _repository;
    private readonly ILogger<ShippingMethodService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ShippingMethodService(
        ApplicationTelemetry telemetry,
        IShippingMethodRepository repository,
        ILogger<ShippingMethodService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ShippingMethod model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ShippingMethod",
                "CreateShippingMethod",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ShippingMethod model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.FlatRate = model.FlatRate;
            existing.EstimatedDays = model.EstimatedDays;
            existing.AsActive = model.AsActive;
            existing.MethodType = model.MethodType;

            await _telemetry.Execute(
                "ShippingMethod",
                "UpdateShippingMethod",
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

    public Task<ShippingMethod?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ShippingMethod>> GetAll(CancellationToken cancellationToken)
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
                "ShippingMethod",
                "UpdateShippingMethod",
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

    public async Task<bool> AssignCarrierService(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ShippingMethod found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CarrierServiceService>().Get(childRequest, cancellationToken);
            parent.CarrierService = child;
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

    public async Task<bool> UnassignCarrierService(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ShippingMethod found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.CarrierService = null;
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


    public async Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ShippingMethod",
                "AddToChannels",
                () => _repository.AddToChannelsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ShippingMethod",
                "RemoveFromChannels",
                () => _repository.RemoveFromChannelsAsync(request, cancellationToken));
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
