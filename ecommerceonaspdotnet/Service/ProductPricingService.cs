using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IProductPricingService {

    Task Create(ProductPricing model , CancellationToken cancellationToken);
    Task<bool> Update(ProductPricing model, CancellationToken cancellationToken);
    Task<ProductPricing?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductPricing>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken);


}

public class ProductPricingService : IProductPricingService
{
    private readonly IProductPricingRepository _repository;
    private readonly ILogger<ProductPricingService> _logger;

    public ProductPricingService(
        IProductPricingRepository repository, ILogger<ProductPricingService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ProductPricing model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ProductPricing model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ListPrice = model.ListPrice;
            existing.SalePrice = model.SalePrice;
            existing.ValidFrom = model.ValidFrom;
            existing.ValidTo = model.ValidTo;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ProductPricing?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ProductPricing>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
