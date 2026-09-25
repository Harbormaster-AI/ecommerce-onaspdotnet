
using ecommerceonaspdotnet.Contracts;
using ecommerceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace ecommerceonaspdotnet.Persistence;

public class MediaAssetRepository : IMediaAssetRepository
{
    private readonly ApplicationDbContext _db;

    public MediaAssetRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MediaAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MediaAssets
            .Include(x => x.Product)
            .Include(x => x.Variant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MediaAsset>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MediaAssets
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Variant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MediaAsset mediaAsset, CancellationToken cancellationToken)
    {
        _db.MediaAssets.Add(mediaAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MediaAsset mediaAsset, CancellationToken cancellationToken)
    {
        _db.MediaAssets.Update(mediaAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MediaAsset mediaAsset, CancellationToken cancellationToken)
    {
        _db.MediaAssets.Remove(mediaAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
