
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CustomerAddressRepository : ICustomerAddressRepository
{
    private readonly ApplicationDbContext _db;

    public CustomerAddressRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CustomerAddress?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CustomerAddresss
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CustomerAddress>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CustomerAddresss
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CustomerAddress customerAddress, CancellationToken cancellationToken)
    {
        _db.CustomerAddresss.Add(customerAddress);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CustomerAddress customerAddress, CancellationToken cancellationToken)
    {
        _db.CustomerAddresss.Update(customerAddress);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CustomerAddress customerAddress, CancellationToken cancellationToken)
    {
        _db.CustomerAddresss.Remove(customerAddress);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
