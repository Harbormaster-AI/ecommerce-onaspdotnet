using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Service;

public interface IPromotionService {

    Task Create(Promotion model , CancellationToken cancellationToken);
    Task<bool> Update(Promotion model, CancellationToken cancellationToken);
    Task<Promotion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Promotion>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToApplicableProducts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApplicableProducts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToApplicableCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApplicableCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCoupons(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCoupons(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _repository;
    private readonly ILogger<PromotionService> _logger;

    public PromotionService(
        IPromotionRepository repository, ILogger<PromotionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Promotion model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Promotion model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Code = model.Code;
            existing.Value = model.Value;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.AsStackable = model.AsStackable;
            existing.MaxRedemptions = model.MaxRedemptions;
            existing.PromotionType = model.PromotionType;
            existing.DiscountType = model.DiscountType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Promotion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Promotion>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToApplicableProducts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApplicableProducts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToApplicableCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApplicableCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCoupons(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCoupons(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
