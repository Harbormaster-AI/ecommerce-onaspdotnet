
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface ICouponService {

    Task Create(Coupon model , CancellationToken cancellationToken);
    Task<bool> Update(Coupon model, CancellationToken cancellationToken);
    Task<Coupon?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Coupon>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPromotion(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPromotion(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CouponService : ICouponService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICouponRepository _repository;
    private readonly ILogger<CouponService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CouponService(
        ApplicationTelemetry telemetry,
        ICouponRepository repository,
        ILogger<CouponService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Coupon model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Coupon",
                "CreateCoupon",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Coupon model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Code = model.Code;
            existing.UsageLimit = model.UsageLimit;
            existing.PerCustomerLimit = model.PerCustomerLimit;
            existing.ExpirationDate = model.ExpirationDate;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "Coupon",
                "UpdateCoupon",
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

    public Task<Coupon?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Coupon>> GetAll(CancellationToken cancellationToken)
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
                "Coupon",
                "UpdateCoupon",
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

    public async Task<bool> AssignPromotion(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Coupon found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<PromotionService>().Get(childRequest, cancellationToken);
            parent.Promotion = child;
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

    public async Task<bool> UnassignPromotion(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Coupon found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Promotion = null;
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


    public async Task<bool> AddToRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Coupon",
                "AddToRedemptions",
                () => _repository.AddToRedemptionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Coupon",
                "RemoveFromRedemptions",
                () => _repository.RemoveFromRedemptionsAsync(request, cancellationToken));
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
