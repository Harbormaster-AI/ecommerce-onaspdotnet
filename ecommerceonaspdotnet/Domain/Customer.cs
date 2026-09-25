
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CustomerId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? Email { get; set; } 
 public virtual string? Phone { get; set; } 
 public virtual bool? MarketingOptIn { get; set; } 
public virtual ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; } = new List<CouponRedemption>();
public virtual ICollection<GiftCard> GiftCards { get; set; } = new List<GiftCard>();
 public virtual CustomerGroup? CustomerGroup { get; set; } 

    public static Customer FromRequest(CustomerRequest request) {
        return new Customer {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            MarketingOptIn = request.MarketingOptIn,
            CustomerGroup = request.CustomerGroup,
        };
    }
}
