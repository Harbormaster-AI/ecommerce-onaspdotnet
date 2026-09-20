using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IProductVariantRepository
{
    Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductVariant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken);
    Task UpdateAsync(ProductVariant productVariant, CancellationToken cancellationToken);
    Task DeleteAsync(ProductVariant productVariant, CancellationToken cancellationToken);
}
