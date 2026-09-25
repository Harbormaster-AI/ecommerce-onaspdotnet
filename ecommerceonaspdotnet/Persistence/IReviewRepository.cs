using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Review>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Review review, CancellationToken cancellationToken);
    Task UpdateAsync(Review review, CancellationToken cancellationToken);
    Task DeleteAsync(Review review, CancellationToken cancellationToken);


}
