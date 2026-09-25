
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class InventoryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InventoryitemId { get; set; } 
 public virtual int? QuantityOnHand { get; set; } 
 public virtual int? QuantityReserved { get; set; } 
 public virtual int? SafetyStock { get; set; } 
public virtual ProductVariant? Variant { get; set; } 
public virtual FulfillmentCenter? FulfillmentCenter { get; set; } 
 public virtual InventoryStatus? Status { get; set; } 

    public static InventoryItem FromRequest(InventoryItemRequest request) {
        return new InventoryItem {
            Id = request.Id,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
            SafetyStock = request.SafetyStock,
            Status = request.Status,
        };
    }
}
