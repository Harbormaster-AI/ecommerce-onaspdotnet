
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _db;

    public ReviewRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Reviews
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Review>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Reviews
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Review review, CancellationToken cancellationToken)
    {
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Review review, CancellationToken cancellationToken)
    {
        _db.Reviews.Update(review);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Review review, CancellationToken cancellationToken)
    {
        _db.Reviews.Remove(review);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
