
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Promotion
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PromotionId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual decimal? Value { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual bool? AsStackable { get; set; } 
 public virtual int? MaxRedemptions { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
public virtual ICollection<Product> ApplicableProducts { get; set; } = new List<Product>();
public virtual ICollection<Category> ApplicableCategories { get; set; } = new List<Category>();
public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
 public virtual PromotionType? PromotionType { get; set; } 
 public virtual DiscountType? DiscountType { get; set; } 

    public static Promotion FromRequest(PromotionRequest request) {
        return new Promotion {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            Value = request.Value,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AsStackable = request.AsStackable,
            MaxRedemptions = request.MaxRedemptions,
            PromotionType = request.PromotionType,
            DiscountType = request.DiscountType,
        };
    }
}
