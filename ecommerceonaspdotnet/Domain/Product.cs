
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Slug { get; set; } 
 public virtual bool? AsActive { get; set; } 
public virtual Brand? Brand { get; set; } 
public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
public virtual ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
public virtual ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
public virtual Seller? Seller { get; set; } 
 public virtual ProductType? ProductType { get; set; } 
 public virtual TaxClass? DefaultTaxClass { get; set; } 

    public static Product FromRequest(ProductRequest request) {
        return new Product {
            Id = request.Id,
            Name = request.Name,
            Slug = request.Slug,
            AsActive = request.AsActive,
            ProductType = request.ProductType,
            DefaultTaxClass = request.DefaultTaxClass,
        };
    }
}
