
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IOrderService {

    Task Create(Order model , CancellationToken cancellationToken);
    Task<bool> Update(Order model, CancellationToken cancellationToken);
    Task<Order?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Order>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSeller(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSeller(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInvoice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInvoice(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToShipments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromShipments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRefunds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRefunds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGiftCardRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGiftCardRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReturnRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReturnRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class OrderService : IOrderService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public OrderService(
        ApplicationTelemetry telemetry,
        IOrderRepository repository,
        ILogger<OrderService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Order model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Order",
                "CreateOrder",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Order model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OrderNumber = model.OrderNumber;
            existing.PlacedDate = model.PlacedDate;
            existing.Subtotal = model.Subtotal;
            existing.DiscountTotal = model.DiscountTotal;
            existing.ShippingTotal = model.ShippingTotal;
            existing.TaxTotal = model.TaxTotal;
            existing.GrandTotal = model.GrandTotal;
            existing.ShippingAddress = model.ShippingAddress;
            existing.BillingAddress = model.BillingAddress;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "Order",
                "UpdateOrder",
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

    public Task<Order?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Order>> GetAll(CancellationToken cancellationToken)
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
                "Order",
                "UpdateOrder",
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

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CustomerService>().Get(childRequest, cancellationToken);
            parent.Customer = child;
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

    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Customer = null;
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

    public async Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ChannelService>().Get(childRequest, cancellationToken);
            parent.Channel = child;
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

    public async Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Channel = null;
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

    public async Task<bool> AssignSeller(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<SellerService>().Get(childRequest, cancellationToken);
            parent.Seller = child;
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

    public async Task<bool> UnassignSeller(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Seller = null;
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

    public async Task<bool> AssignInvoice(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<InvoiceService>().Get(childRequest, cancellationToken);
            parent.Invoice = child;
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

    public async Task<bool> UnassignInvoice(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Order found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Invoice = null;
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


    public async Task<bool> AddToOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToOrderLines",
                () => _repository.AddToOrderLinesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromOrderLines",
                () => _repository.RemoveFromOrderLinesAsync(request, cancellationToken));
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

    public async Task<bool> AddToPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToPayments",
                () => _repository.AddToPaymentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromPayments",
                () => _repository.RemoveFromPaymentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToShipments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToShipments",
                () => _repository.AddToShipmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromShipments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromShipments",
                () => _repository.RemoveFromShipmentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToRefunds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToRefunds",
                () => _repository.AddToRefundsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRefunds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromRefunds",
                () => _repository.RemoveFromRefundsAsync(request, cancellationToken));
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
                "Order",
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
                "Order",
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

    public async Task<bool> AddToGiftCardRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToGiftCardRedemptions",
                () => _repository.AddToGiftCardRedemptionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromGiftCardRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromGiftCardRedemptions",
                () => _repository.RemoveFromGiftCardRedemptionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToCouponRedemptions",
                () => _repository.AddToCouponRedemptionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromCouponRedemptions",
                () => _repository.RemoveFromCouponRedemptionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToReturnRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "AddToReturnRequests",
                () => _repository.AddToReturnRequestsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromReturnRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Order",
                "RemoveFromReturnRequests",
                () => _repository.RemoveFromReturnRequestsAsync(request, cancellationToken));
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
