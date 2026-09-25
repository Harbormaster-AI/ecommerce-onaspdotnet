using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IPromotionRepository
{
    Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Promotion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Promotion promotion, CancellationToken cancellationToken);
    Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken);
    Task DeleteAsync(Promotion promotion, CancellationToken cancellationToken);

    Task AddToChannelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromChannelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToApplicableProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApplicableProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToApplicableCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApplicableCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCouponsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCouponsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
