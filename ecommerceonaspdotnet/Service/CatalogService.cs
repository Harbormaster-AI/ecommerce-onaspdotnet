using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface ICatalogService {

    Task Create(Catalog model , CancellationToken cancellationToken);
    Task<bool> Update(Catalog model, CancellationToken cancellationToken);
    Task<Catalog?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Catalog>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _repository;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(
        ICatalogRepository repository, ILogger<CatalogService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Catalog model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Catalog model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.CatalogCode = model.CatalogCode;
            existing.AsActive = model.AsActive;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Catalog?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Catalog>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
