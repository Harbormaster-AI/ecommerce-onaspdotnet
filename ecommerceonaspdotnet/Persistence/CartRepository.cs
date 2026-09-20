using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;

    public CartRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Carts
            .Include(x => x.Customer)
            .Include(x => x.Channel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Cart>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Carts
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Channel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Cart cart, CancellationToken cancellationToken)
    {
        _db.Carts.Add(cart);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken)
    {
        _db.Carts.Update(cart);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken)
    {
        _db.Carts.Remove(cart);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
