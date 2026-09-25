
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface ICarrierServiceService {

    Task Create(CarrierService model , CancellationToken cancellationToken);
    Task<bool> Update(CarrierService model, CancellationToken cancellationToken);
    Task<CarrierService?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarrierService>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CarrierServiceService : ICarrierServiceService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICarrierServiceRepository _repository;
    private readonly ILogger<CarrierServiceService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CarrierServiceService(
        ApplicationTelemetry telemetry,
        ICarrierServiceRepository repository,
        ILogger<CarrierServiceService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CarrierService model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CarrierService",
                "CreateCarrierService",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CarrierService model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Code = model.Code;
            existing.Carrier = model.Carrier;
            existing.ServiceLevel = model.ServiceLevel;

            await _telemetry.Execute(
                "CarrierService",
                "UpdateCarrierService",
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

    public Task<CarrierService?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CarrierService>> GetAll(CancellationToken cancellationToken)
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
                "CarrierService",
                "UpdateCarrierService",
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


    public async Task<bool> AddToShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CarrierService",
                "AddToShippingMethods",
                () => _repository.AddToShippingMethodsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CarrierService",
                "RemoveFromShippingMethods",
                () => _repository.RemoveFromShippingMethodsAsync(request, cancellationToken));
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
