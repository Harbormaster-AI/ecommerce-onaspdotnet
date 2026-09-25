
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IOrderLineService {

    Task Create(OrderLine model , CancellationToken cancellationToken);
    Task<bool> Update(OrderLine model, CancellationToken cancellationToken);
    Task<OrderLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<OrderLine>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class OrderLineService : IOrderLineService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IOrderLineRepository _repository;
    private readonly ILogger<OrderLineService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public OrderLineService(
        ApplicationTelemetry telemetry,
        IOrderLineRepository repository,
        ILogger<OrderLineService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(OrderLine model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "OrderLine",
                "CreateOrderLine",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(OrderLine model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Quantity = model.Quantity;
            existing.UnitPrice = model.UnitPrice;
            existing.TotalPrice = model.TotalPrice;
            existing.TaxRate = model.TaxRate;
            existing.LineStatus = model.LineStatus;

            await _telemetry.Execute(
                "OrderLine",
                "UpdateOrderLine",
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

    public Task<OrderLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<OrderLine>> GetAll(CancellationToken cancellationToken)
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
                "OrderLine",
                "UpdateOrderLine",
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

    public async Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No OrderLine found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<OrderService>().Get(childRequest, cancellationToken);
            parent.Order = child;
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

    public async Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No OrderLine found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Order = null;
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

    public async Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No OrderLine found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ProductVariantService>().Get(childRequest, cancellationToken);
            parent.Variant = child;
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

    public async Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No OrderLine found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Variant = null;
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


    public async Task<bool> AddToAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "OrderLine",
                "AddToAppliedPromotions",
                () => _repository.AddToAppliedPromotionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "OrderLine",
                "RemoveFromAppliedPromotions",
                () => _repository.RemoveFromAppliedPromotionsAsync(request, cancellationToken));
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
