using ecommerceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class ChannelRepository : IChannelRepository
{
    private readonly ApplicationDbContext _db;

    public ChannelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Channels
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Channel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Channels
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Channel channel, CancellationToken cancellationToken)
    {
        _db.Channels.Add(channel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Channel channel, CancellationToken cancellationToken)
    {
        _db.Channels.Update(channel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Channel channel, CancellationToken cancellationToken)
    {
        _db.Channels.Remove(channel);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
