
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPromotionRepository _repository;
    private readonly ILogger<PromotionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PromotionService(
        ApplicationTelemetry telemetry,
        IPromotionRepository repository,
        ILogger<PromotionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Promotion model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Promotion",
                "CreatePromotion",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "Promotion",
                "UpdatePromotion",
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
            await _telemetry.Execute(
                "Promotion",
                "UpdatePromotion",
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

    public async Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Promotion found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<MerchantService>().Get(childRequest, cancellationToken);
            parent.Merchant = child;
            await Update( parent, cancellationToken );
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

    public async Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Promotion found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Merchant = null;
            await Update( parent, cancellationToken );
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


    public async Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "AddToChannels",
                () => _repository.AddToChannelsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "RemoveFromChannels",
                () => _repository.RemoveFromChannelsAsync(request, cancellationToken));
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

    public async Task<bool> AddToApplicableProducts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "AddToApplicableProducts",
                () => _repository.AddToApplicableProductsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromApplicableProducts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "RemoveFromApplicableProducts",
                () => _repository.RemoveFromApplicableProductsAsync(request, cancellationToken));
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

    public async Task<bool> AddToApplicableCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "AddToApplicableCategories",
                () => _repository.AddToApplicableCategoriesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromApplicableCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "RemoveFromApplicableCategories",
                () => _repository.RemoveFromApplicableCategoriesAsync(request, cancellationToken));
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

    public async Task<bool> AddToCoupons(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "AddToCoupons",
                () => _repository.AddToCouponsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCoupons(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Promotion",
                "RemoveFromCoupons",
                () => _repository.RemoveFromCouponsAsync(request, cancellationToken));
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
