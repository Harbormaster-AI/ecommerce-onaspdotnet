using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IShipmentService {

    Task Create(Shipment model , CancellationToken cancellationToken);
    Task<bool> Update(Shipment model, CancellationToken cancellationToken);
    Task<Shipment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Shipment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToShipmentItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromShipmentItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _repository;
    private readonly ILogger<ShipmentService> _logger;

    public ShipmentService(
        IShipmentRepository repository, ILogger<ShipmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Shipment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Shipment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ShipmentNumber = model.ShipmentNumber;
            existing.ShippedDate = model.ShippedDate;
            existing.DeliveredDate = model.DeliveredDate;
            existing.TrackingNumber = model.TrackingNumber;
            existing.ShippingAddress = model.ShippingAddress;
            existing.Status = model.Status;
            existing.Carrier = model.Carrier;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Shipment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Shipment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignFulfillmentCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToShipmentItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromShipmentItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
