
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Wishlist
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WishlistId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual bool? AsPublic { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();

    public static Wishlist FromRequest(WishlistRequest request) {
        return new Wishlist {
            Id = request.Id,
            Name = request.Name,
            AsPublic = request.AsPublic,
            CreatedAt = request.CreatedAt,
        };
    }
}
