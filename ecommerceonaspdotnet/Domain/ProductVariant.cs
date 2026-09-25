
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class ProductVariant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductvariantId { get; set; } 
 public virtual SKU? Sku { get; set; } 
 public virtual string? Barcode { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual decimal? Weight { get; set; } 
 public virtual bool? RequiresShipping { get; set; } 
public virtual Product? Product { get; set; } 
public virtual ICollection<ProductPricing> Pricing { get; set; } = new List<ProductPricing>();
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
public virtual ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
 public virtual WeightUnit? WeightUnit { get; set; } 

    public static ProductVariant FromRequest(ProductVariantRequest request) {
        return new ProductVariant {
            Id = request.Id,
            Sku = request.Sku,
            Barcode = request.Barcode,
            Title = request.Title,
            Weight = request.Weight,
            RequiresShipping = request.RequiresShipping,
            WeightUnit = request.WeightUnit,
        };
    }
}
