
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Payout
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PayoutId { get; set; } 
 public virtual string? PayoutNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? ScheduledDate { get; set; } 
 public virtual DateOnly? PaidDate { get; set; } 
public virtual Seller? Seller { get; set; } 
public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
 public virtual PayoutStatus? Status { get; set; } 

    public static Payout FromRequest(PayoutRequest request) {
        return new Payout {
            Id = request.Id,
            PayoutNumber = request.PayoutNumber,
            Amount = request.Amount,
            ScheduledDate = request.ScheduledDate,
            PaidDate = request.PaidDate,
            Status = request.Status,
        };
    }
}
