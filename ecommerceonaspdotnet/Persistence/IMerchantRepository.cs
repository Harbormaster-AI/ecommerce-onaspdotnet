using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IMerchantRepository
{
    Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Merchant merchant, CancellationToken cancellationToken);
    Task UpdateAsync(Merchant merchant, CancellationToken cancellationToken);
    Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken);

    Task AddToChannelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromChannelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBrandsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBrandsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFulfillmentCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFulfillmentCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTaxRulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTaxRulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSellersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSellersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
