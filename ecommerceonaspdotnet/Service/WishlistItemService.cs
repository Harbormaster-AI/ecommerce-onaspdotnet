using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IWishlistItemService {

    Task Create(WishlistItem model , CancellationToken cancellationToken);
    Task<bool> Update(WishlistItem model, CancellationToken cancellationToken);
    Task<WishlistItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<WishlistItem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignWishlist(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWishlist(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken);


}

public class WishlistItemService : IWishlistItemService
{
    private readonly IWishlistItemRepository _repository;
    private readonly ILogger<WishlistItemService> _logger;

    public WishlistItemService(
        IWishlistItemRepository repository, ILogger<WishlistItemService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(WishlistItem model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(WishlistItem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AddedDate = model.AddedDate;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<WishlistItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<WishlistItem>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignWishlist(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWishlist(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
