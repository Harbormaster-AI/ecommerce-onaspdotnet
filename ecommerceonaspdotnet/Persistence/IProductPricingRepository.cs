using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IProductPricingRepository
{
    Task<ProductPricing?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductPricing>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductPricing productPricing, CancellationToken cancellationToken);
    Task UpdateAsync(ProductPricing productPricing, CancellationToken cancellationToken);
    Task DeleteAsync(ProductPricing productPricing, CancellationToken cancellationToken);


}
