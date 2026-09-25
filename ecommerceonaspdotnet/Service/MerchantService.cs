
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface IMerchantService {

    Task Create(Merchant model , CancellationToken cancellationToken);
    Task<bool> Update(Merchant model, CancellationToken cancellationToken);
    Task<Merchant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Merchant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBrands(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBrands(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFulfillmentCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFulfillmentCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTaxRules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTaxRules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSellers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSellers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPromotions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MerchantService : IMerchantService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IMerchantRepository _repository;
    private readonly ILogger<MerchantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public MerchantService(
        ApplicationTelemetry telemetry,
        IMerchantRepository repository,
        ILogger<MerchantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Merchant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Merchant",
                "CreateMerchant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Merchant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.Website = model.Website;
            existing.DefaultCurrency = model.DefaultCurrency;
            existing.DefaultLocale = model.DefaultLocale;
            existing.SupportEmail = model.SupportEmail;

            await _telemetry.Execute(
                "Merchant",
                "UpdateMerchant",
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

    public Task<Merchant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Merchant>> GetAll(CancellationToken cancellationToken)
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
                "Merchant",
                "UpdateMerchant",
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


    public async Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
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
                "Merchant",
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

    public async Task<bool> AddToBrands(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToBrands",
                () => _repository.AddToBrandsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromBrands(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromBrands",
                () => _repository.RemoveFromBrandsAsync(request, cancellationToken));
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

    public async Task<bool> AddToFulfillmentCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToFulfillmentCenters",
                () => _repository.AddToFulfillmentCentersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromFulfillmentCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromFulfillmentCenters",
                () => _repository.RemoveFromFulfillmentCentersAsync(request, cancellationToken));
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

    public async Task<bool> AddToTaxRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToTaxRules",
                () => _repository.AddToTaxRulesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromTaxRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromTaxRules",
                () => _repository.RemoveFromTaxRulesAsync(request, cancellationToken));
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
                "Merchant",
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
                "Merchant",
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

    public async Task<bool> AddToSellers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToSellers",
                () => _repository.AddToSellersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSellers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromSellers",
                () => _repository.RemoveFromSellersAsync(request, cancellationToken));
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
                "Merchant",
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
                "Merchant",
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



}
