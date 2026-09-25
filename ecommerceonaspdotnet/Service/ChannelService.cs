
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IChannelService {

    Task Create(Channel model , CancellationToken cancellationToken);
    Task<bool> Update(Channel model, CancellationToken cancellationToken);
    Task<Channel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Channel>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCatalogs(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCatalogs(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ChannelService : IChannelService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IChannelRepository _repository;
    private readonly ILogger<ChannelService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ChannelService(
        ApplicationTelemetry telemetry,
        IChannelRepository repository,
        ILogger<ChannelService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Channel model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Channel",
                "CreateChannel",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Channel model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ChannelCode = model.ChannelCode;
            existing.Locale = model.Locale;
            existing.Domain = model.Domain;
            existing.AsActive = model.AsActive;
            existing.DefaultCurrency = model.DefaultCurrency;
            existing.ChannelType = model.ChannelType;

            await _telemetry.Execute(
                "Channel",
                "UpdateChannel",
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

    public Task<Channel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Channel>> GetAll(CancellationToken cancellationToken)
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
                "Channel",
                "UpdateChannel",
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
            _logger.LogError("No Channel found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Channel found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToCatalogs(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "AddToCatalogs",
                () => _repository.AddToCatalogsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCatalogs(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "RemoveFromCatalogs",
                () => _repository.RemoveFromCatalogsAsync(request, cancellationToken));
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

    public async Task<bool> AddToPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "AddToPromotions",
                () => _repository.AddToPromotionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "RemoveFromPromotions",
                () => _repository.RemoveFromPromotionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "AddToShippingMethods",
                () => _repository.AddToShippingMethodsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "RemoveFromShippingMethods",
                () => _repository.RemoveFromShippingMethodsAsync(request, cancellationToken));
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

    public async Task<bool> AddToPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "AddToPaymentProviders",
                () => _repository.AddToPaymentProvidersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Channel",
                "RemoveFromPaymentProviders",
                () => _repository.RemoveFromPaymentProvidersAsync(request, cancellationToken));
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
