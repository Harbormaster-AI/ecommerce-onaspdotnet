using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface ICustomerAddressRepository
{
    Task<CustomerAddress?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CustomerAddress>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CustomerAddress customerAddress, CancellationToken cancellationToken);
    Task UpdateAsync(CustomerAddress customerAddress, CancellationToken cancellationToken);
    Task DeleteAsync(CustomerAddress customerAddress, CancellationToken cancellationToken);


}
