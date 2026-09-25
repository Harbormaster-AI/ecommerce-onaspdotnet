
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class ReturnItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ReturnitemId { get; set; } 
 public virtual int? Quantity { get; set; } 
public virtual ReturnRequest? ReturnRequest { get; set; } 
public virtual OrderLine? OrderLine { get; set; } 
 public virtual ReturnReason? Reason { get; set; } 
 public virtual ReturnItemCondition? Condition { get; set; } 

    public static ReturnItem FromRequest(ReturnItemRequest request) {
        return new ReturnItem {
            Id = request.Id,
            Quantity = request.Quantity,
            Reason = request.Reason,
            Condition = request.Condition,
        };
    }
}
