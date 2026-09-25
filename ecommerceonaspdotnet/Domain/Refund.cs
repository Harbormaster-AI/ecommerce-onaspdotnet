
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Refund
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RefundId { get; set; } 
 public virtual string? RefundNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
public virtual Payment? Payment { get; set; } 
public virtual Order? Order { get; set; } 
 public virtual RefundStatus? Status { get; set; } 

    public static Refund FromRequest(RefundRequest request) {
        return new Refund {
            Id = request.Id,
            RefundNumber = request.RefundNumber,
            Amount = request.Amount,
            Reason = request.Reason,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
        };
    }
}
