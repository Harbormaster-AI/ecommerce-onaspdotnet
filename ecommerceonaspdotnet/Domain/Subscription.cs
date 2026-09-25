
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Subscription
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SubscriptionId { get; set; } 
 public virtual string? SubscriptionNumber { get; set; } 
 public virtual DateOnly? NextBillingDate { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ProductVariant? Variant { get; set; } 
public virtual PaymentProvider? PaymentProvider { get; set; } 
public virtual Channel? Channel { get; set; } 
 public virtual SubscriptionStatus? Status { get; set; } 
 public virtual SubscriptionInterval? Interval { get; set; } 

    public static Subscription FromRequest(SubscriptionRequest request) {
        return new Subscription {
            Id = request.Id,
            SubscriptionNumber = request.SubscriptionNumber,
            NextBillingDate = request.NextBillingDate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            Interval = request.Interval,
        };
    }
}
