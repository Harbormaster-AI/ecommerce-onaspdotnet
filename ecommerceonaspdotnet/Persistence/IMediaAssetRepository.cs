using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Persistence;

public interface IMediaAssetRepository
{
    Task<MediaAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MediaAsset>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MediaAsset mediaAsset, CancellationToken cancellationToken);
    Task UpdateAsync(MediaAsset mediaAsset, CancellationToken cancellationToken);
    Task DeleteAsync(MediaAsset mediaAsset, CancellationToken cancellationToken);


}
