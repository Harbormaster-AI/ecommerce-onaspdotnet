using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IPromotionRepository
{
    Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Promotion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Promotion promotion, CancellationToken cancellationToken);
    Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken);
    Task DeleteAsync(Promotion promotion, CancellationToken cancellationToken);
}
