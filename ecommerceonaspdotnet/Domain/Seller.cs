
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Seller
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SellerId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? SellerCode { get; set; } 
 public virtual string? ContactEmail { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Product> Products { get; set; } = new List<Product>();
public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
 public virtual SellerStatus? Status { get; set; } 

    public static Seller FromRequest(SellerRequest request) {
        return new Seller {
            Id = request.Id,
            Name = request.Name,
            SellerCode = request.SellerCode,
            ContactEmail = request.ContactEmail,
            Status = request.Status,
        };
    }
}
