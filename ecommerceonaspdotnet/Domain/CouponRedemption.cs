
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class CouponRedemption
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CouponredemptionId { get; set; } 
 public virtual DateOnly? RedeemedAt { get; set; } 
public virtual Coupon? Coupon { get; set; } 
public virtual Order? Order { get; set; } 
public virtual Customer? Customer { get; set; } 

    public static CouponRedemption FromRequest(CouponRedemptionRequest request) {
        return new CouponRedemption {
            Id = request.Id,
            RedeemedAt = request.RedeemedAt,
        };
    }
}
