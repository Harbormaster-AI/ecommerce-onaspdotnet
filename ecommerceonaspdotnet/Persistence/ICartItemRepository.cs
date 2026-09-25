using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICartItemRepository
{
    Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CartItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CartItem cartItem, CancellationToken cancellationToken);
    Task UpdateAsync(CartItem cartItem, CancellationToken cancellationToken);
    Task DeleteAsync(CartItem cartItem, CancellationToken cancellationToken);

    Task AddToAppliedPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAppliedPromotionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
