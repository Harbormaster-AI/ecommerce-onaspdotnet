using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

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
    private readonly ICarrierServiceRepository _repository;
    private readonly ILogger<CarrierServiceService> _logger;

    public CarrierServiceService(
        ICarrierServiceRepository repository, ILogger<CarrierServiceService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CarrierService model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }


    public async Task<bool> AddToShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
