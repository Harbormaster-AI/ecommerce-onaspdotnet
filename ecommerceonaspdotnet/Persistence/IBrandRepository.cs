using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Brand>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Brand brand, CancellationToken cancellationToken);
    Task UpdateAsync(Brand brand, CancellationToken cancellationToken);
    Task DeleteAsync(Brand brand, CancellationToken cancellationToken);
}
