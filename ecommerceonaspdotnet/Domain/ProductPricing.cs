
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class ProductPricing
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductpricingId { get; set; } 
 public virtual Money? ListPrice { get; set; } 
 public virtual Money? SalePrice { get; set; } 
 public virtual DateOnly? ValidFrom { get; set; } 
 public virtual DateOnly? ValidTo { get; set; } 
public virtual ProductVariant? Variant { get; set; } 
public virtual Channel? Channel { get; set; } 

    public static ProductPricing FromRequest(ProductPricingRequest request) {
        return new ProductPricing {
            Id = request.Id,
            ListPrice = request.ListPrice,
            SalePrice = request.SalePrice,
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo,
        };
    }
}
