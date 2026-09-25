
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface ICustomerService {

    Task Create(Customer model , CancellationToken cancellationToken);
    Task<bool> Update(Customer model, CancellationToken cancellationToken);
    Task<Customer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCarts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCarts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWishlists(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWishlists(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCouponRedemptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGiftCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGiftCards(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CustomerService : ICustomerService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICustomerRepository _repository;
    private readonly ILogger<CustomerService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CustomerService(
        ApplicationTelemetry telemetry,
        ICustomerRepository repository,
        ILogger<CustomerService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Customer model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Customer",
                "CreateCustomer",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Customer model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.MarketingOptIn = model.MarketingOptIn;
            existing.CustomerGroup = model.CustomerGroup;

            await _telemetry.Execute(
                "Customer",
                "UpdateCustomer",
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

    public Task<Customer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken)
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
                "Customer",
                "UpdateCustomer",
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


    public async Task<bool> AddToAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToAddresses",
                () => _repository.AddToAddressesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromAddresses",
                () => _repository.RemoveFromAddressesAsync(request, cancellationToken));
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

    public async Task<bool> AddToCarts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToCarts",
                () => _repository.AddToCartsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCarts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromCarts",
                () => _repository.RemoveFromCartsAsync(request, cancellationToken));
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

    public async Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToOrders",
                () => _repository.AddToOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromOrders",
                () => _repository.RemoveFromOrdersAsync(request, cancellationToken));
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
                "Customer",
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
                "Customer",
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

    public async Task<bool> AddToReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToReviews",
                () => _repository.AddToReviewsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromReviews",
                () => _repository.RemoveFromReviewsAsync(request, cancellationToken));
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

    public async Task<bool> AddToWishlists(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToWishlists",
                () => _repository.AddToWishlistsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromWishlists(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromWishlists",
                () => _repository.RemoveFromWishlistsAsync(request, cancellationToken));
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

    public async Task<bool> AddToSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToSubscriptions",
                () => _repository.AddToSubscriptionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSubscriptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromSubscriptions",
                () => _repository.RemoveFromSubscriptionsAsync(request, cancellationToken));
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
                "Customer",
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
                "Customer",
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

    public async Task<bool> AddToGiftCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToGiftCards",
                () => _repository.AddToGiftCardsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromGiftCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromGiftCards",
                () => _repository.RemoveFromGiftCardsAsync(request, cancellationToken));
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
