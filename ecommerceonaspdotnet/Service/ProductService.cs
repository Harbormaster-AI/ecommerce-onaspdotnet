using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IProductService {

    Task Create(Product model , CancellationToken cancellationToken);
    Task<bool> Update(Product model, CancellationToken cancellationToken);
    Task<Product?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBrand(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBrand(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSeller(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSeller(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository repository, ILogger<ProductService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Product model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Product model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Slug = model.Slug;
            existing.AsActive = model.AsActive;
            existing.ProductType = model.ProductType;
            existing.DefaultTaxClass = model.DefaultTaxClass;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Product?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignBrand(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBrand(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSeller(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSeller(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMediaAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
