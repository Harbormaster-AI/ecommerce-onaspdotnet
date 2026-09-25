
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class CartItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CartitemId { get; set; } 
 public virtual int? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual Money? TotalPrice { get; set; } 
public virtual Cart? Cart { get; set; } 
public virtual ProductVariant? Variant { get; set; } 
public virtual ICollection<Promotion> AppliedPromotions { get; set; } = new List<Promotion>();

    public static CartItem FromRequest(CartItemRequest request) {
        return new CartItem {
            Id = request.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            TotalPrice = request.TotalPrice,
        };
    }
}
