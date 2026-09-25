
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Coupon
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CouponId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual int? UsageLimit { get; set; } 
 public virtual int? PerCustomerLimit { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
public virtual Promotion? Promotion { get; set; } 
public virtual ICollection<CouponRedemption> Redemptions { get; set; } = new List<CouponRedemption>();
 public virtual CouponStatus? Status { get; set; } 

    public static Coupon FromRequest(CouponRequest request) {
        return new Coupon {
            Id = request.Id,
            Code = request.Code,
            UsageLimit = request.UsageLimit,
            PerCustomerLimit = request.PerCustomerLimit,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
        };
    }
}
