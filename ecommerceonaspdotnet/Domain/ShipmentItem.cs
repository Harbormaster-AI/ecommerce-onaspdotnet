
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class ShipmentItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ShipmentitemId { get; set; } 
 public virtual int? Quantity { get; set; } 
public virtual Shipment? Shipment { get; set; } 
public virtual OrderLine? OrderLine { get; set; } 

    public static ShipmentItem FromRequest(ShipmentItemRequest request) {
        return new ShipmentItem {
            Id = request.Id,
            Quantity = request.Quantity,
        };
    }
}
