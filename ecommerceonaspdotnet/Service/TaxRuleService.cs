
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Telemetry;

namespace ecommerceonaspdotnet.Service;

public interface ITaxRuleService {

    Task Create(TaxRule model , CancellationToken cancellationToken);
    Task<bool> Update(TaxRule model, CancellationToken cancellationToken);
    Task<TaxRule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TaxRule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChannels(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TaxRuleService : ITaxRuleService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ITaxRuleRepository _repository;
    private readonly ILogger<TaxRuleService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public TaxRuleService(
        ApplicationTelemetry telemetry,
        ITaxRuleRepository repository,
        ILogger<TaxRuleService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(TaxRule model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "TaxRule",
                "CreateTaxRule",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(TaxRule model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Country = model.Country;
            existing.Region = model.Region;
            existing.Rate = model.Rate;
            existing.TaxInclusive = model.TaxInclusive;
            existing.TaxClass = model.TaxClass;

            await _telemetry.Execute(
                "TaxRule",
                "UpdateTaxRule",
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

    public Task<TaxRule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TaxRule>> GetAll(CancellationToken cancellationToken)
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
                "TaxRule",
                "UpdateTaxRule",
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
            _logger.LogError("No TaxRule found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No TaxRule found using Id {ParentId}", request.ParentId);
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
                "TaxRule",
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
                "TaxRule",
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



}
