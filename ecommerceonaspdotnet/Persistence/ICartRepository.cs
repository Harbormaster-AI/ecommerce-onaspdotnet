using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Cart>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Cart cart, CancellationToken cancellationToken);
    Task UpdateAsync(Cart cart, CancellationToken cancellationToken);
    Task DeleteAsync(Cart cart, CancellationToken cancellationToken);

    Task AddToItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAppliedPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAppliedPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
