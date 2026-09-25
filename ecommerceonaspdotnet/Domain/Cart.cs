
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Cart
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CartId { get; set; } 
 public virtual string? CartNumber { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual Address? ShippingAddress { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Channel? Channel { get; set; } 
public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
public virtual ICollection<Promotion> AppliedPromotions { get; set; } = new List<Promotion>();
 public virtual CartStatus? Status { get; set; } 

    public static Cart FromRequest(CartRequest request) {
        return new Cart {
            Id = request.Id,
            CartNumber = request.CartNumber,
            CreatedAt = request.CreatedAt,
            Currency = request.Currency,
            ShippingAddress = request.ShippingAddress,
            BillingAddress = request.BillingAddress,
            Status = request.Status,
        };
    }
}
