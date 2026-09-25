
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentId { get; set; } 
 public virtual string? PaymentNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? TransactionId { get; set; } 
 public virtual DateOnly? AuthorizedAt { get; set; } 
 public virtual DateOnly? CapturedAt { get; set; } 
public virtual Order? Order { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual PaymentProvider? PaymentProvider { get; set; } 
public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
 public virtual PaymentStatus? Status { get; set; } 
 public virtual PaymentMethodType? PaymentMethod { get; set; } 

    public static Payment FromRequest(PaymentRequest request) {
        return new Payment {
            Id = request.Id,
            PaymentNumber = request.PaymentNumber,
            Amount = request.Amount,
            TransactionId = request.TransactionId,
            AuthorizedAt = request.AuthorizedAt,
            CapturedAt = request.CapturedAt,
            Status = request.Status,
            PaymentMethod = request.PaymentMethod,
        };
    }
}
