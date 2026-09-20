using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;

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
    private readonly IChannelRepository _repository;
    private readonly ILogger<ChannelService> _logger;

    public ChannelService(
        IChannelRepository repository, ILogger<ChannelService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Channel model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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


    public async Task<bool> AddToCatalogs(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCatalogs(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromShippingMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
