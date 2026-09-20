using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IShipmentItemService {

    Task Create(ShipmentItem model , CancellationToken cancellationToken);
    Task<bool> Update(ShipmentItem model, CancellationToken cancellationToken);
    Task<ShipmentItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ShipmentItem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignShipment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignShipment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOrderLine(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrderLine(AssociationRequest request, CancellationToken cancellationToken);


}

public class ShipmentItemService : IShipmentItemService
{
    private readonly IShipmentItemRepository _repository;
    private readonly ILogger<ShipmentItemService> _logger;

    public ShipmentItemService(
        IShipmentItemRepository repository, ILogger<ShipmentItemService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ShipmentItem model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ShipmentItem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Quantity = model.Quantity;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ShipmentItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ShipmentItem>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignShipment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignShipment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignOrderLine(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrderLine(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
