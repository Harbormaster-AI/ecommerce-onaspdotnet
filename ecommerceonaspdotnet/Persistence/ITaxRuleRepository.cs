using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface ITaxRuleRepository
{
    Task<TaxRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TaxRule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TaxRule taxRule, CancellationToken cancellationToken);
    Task UpdateAsync(TaxRule taxRule, CancellationToken cancellationToken);
    Task DeleteAsync(TaxRule taxRule, CancellationToken cancellationToken);
}
