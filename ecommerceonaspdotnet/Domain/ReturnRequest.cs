
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class ReturnRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ReturnrequestId { get; set; } 
 public virtual string? ReturnNumber { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
 public virtual Money? RefundAmount { get; set; } 
public virtual Order? Order { get; set; } 
public virtual ICollection<ReturnItem> Items { get; set; } = new List<ReturnItem>();
public virtual Refund? Refund { get; set; } 
public virtual Shipment? Shipment { get; set; } 
 public virtual ReturnStatus? Status { get; set; } 

    public static ReturnRequest FromRequest(ReturnRequestRequest request) {
        return new ReturnRequest {
            Id = request.Id,
            ReturnNumber = request.ReturnNumber,
            CreatedAt = request.CreatedAt,
            RefundAmount = request.RefundAmount,
            Status = request.Status,
        };
    }
}
