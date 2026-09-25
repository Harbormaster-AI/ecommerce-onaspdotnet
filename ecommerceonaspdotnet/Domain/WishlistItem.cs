
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class WishlistItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WishlistitemId { get; set; } 
 public virtual DateOnly? AddedDate { get; set; } 
public virtual Wishlist? Wishlist { get; set; } 
public virtual ProductVariant? Variant { get; set; } 

    public static WishlistItem FromRequest(WishlistItemRequest request) {
        return new WishlistItem {
            Id = request.Id,
            AddedDate = request.AddedDate,
        };
    }
}
