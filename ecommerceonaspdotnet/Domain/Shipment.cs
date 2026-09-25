
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Shipment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ShipmentId { get; set; } 
 public virtual string? ShipmentNumber { get; set; } 
 public virtual DateOnly? ShippedDate { get; set; } 
 public virtual DateOnly? DeliveredDate { get; set; } 
 public virtual string? TrackingNumber { get; set; } 
 public virtual Address? ShippingAddress { get; set; } 
public virtual Order? Order { get; set; } 
public virtual ICollection<ShipmentItem> ShipmentItems { get; set; } = new List<ShipmentItem>();
public virtual FulfillmentCenter? FulfillmentCenter { get; set; } 
 public virtual ShipmentStatus? Status { get; set; } 
 public virtual Carrier? Carrier { get; set; } 

    public static Shipment FromRequest(ShipmentRequest request) {
        return new Shipment {
            Id = request.Id,
            ShipmentNumber = request.ShipmentNumber,
            ShippedDate = request.ShippedDate,
            DeliveredDate = request.DeliveredDate,
            TrackingNumber = request.TrackingNumber,
            ShippingAddress = request.ShippingAddress,
            Status = request.Status,
            Carrier = request.Carrier,
        };
    }
}
