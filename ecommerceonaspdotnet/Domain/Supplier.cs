
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Supplier
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SupplierId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ContactEmail { get; set; } 
 public virtual string? Website { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Product> Products { get; set; } = new List<Product>();
public virtual ICollection<FulfillmentCenter> FulfillmentCenters { get; set; } = new List<FulfillmentCenter>();
 public virtual SupplierStatus? Status { get; set; } 

    public static Supplier FromRequest(SupplierRequest request) {
        return new Supplier {
            Id = request.Id,
            Name = request.Name,
            ContactEmail = request.ContactEmail,
            Website = request.Website,
            Status = request.Status,
        };
    }
}
