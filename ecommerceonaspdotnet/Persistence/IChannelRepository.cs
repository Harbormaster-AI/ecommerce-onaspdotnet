using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public interface IChannelRepository
{
    Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Channel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Channel channel, CancellationToken cancellationToken);
    Task UpdateAsync(Channel channel, CancellationToken cancellationToken);
    Task DeleteAsync(Channel channel, CancellationToken cancellationToken);
}
