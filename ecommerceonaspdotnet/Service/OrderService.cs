using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

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
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository repository, ILogger<OrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Order model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignChannel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSeller(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSeller(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInvoice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInvoice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOrderLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToShipments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromShipments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToRefunds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRefunds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAppliedPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToGiftCardRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromGiftCardRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToReturnRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromReturnRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
