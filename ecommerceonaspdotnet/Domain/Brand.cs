
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Brand
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BrandId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual string? Website { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public static Brand FromRequest(BrandRequest request) {
        return new Brand {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Website = request.Website,
        };
    }
}
