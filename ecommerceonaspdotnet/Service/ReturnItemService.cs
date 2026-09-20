using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IReturnItemService {

    Task Create(ReturnItem model , CancellationToken cancellationToken);
    Task<bool> Update(ReturnItem model, CancellationToken cancellationToken);
    Task<ReturnItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ReturnItem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignReturnRequest(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignReturnRequest(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOrderLine(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrderLine(AssociationRequest request, CancellationToken cancellationToken);


}

public class ReturnItemService : IReturnItemService
{
    private readonly IReturnItemRepository _repository;
    private readonly ILogger<ReturnItemService> _logger;

    public ReturnItemService(
        IReturnItemRepository repository, ILogger<ReturnItemService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ReturnItem model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ReturnItem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Quantity = model.Quantity;
            existing.Reason = model.Reason;
            existing.Condition = model.Condition;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ReturnItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ReturnItem>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignReturnRequest(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignReturnRequest(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignOrderLine(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrderLine(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
