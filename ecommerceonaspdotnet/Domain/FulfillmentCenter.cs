
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class FulfillmentCenter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FulfillmentcenterId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? CenterCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? Timezone { get; set; } 
 public virtual bool? AsActive { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public static FulfillmentCenter FromRequest(FulfillmentCenterRequest request) {
        return new FulfillmentCenter {
            Id = request.Id,
            Name = request.Name,
            CenterCode = request.CenterCode,
            Address = request.Address,
            Timezone = request.Timezone,
            AsActive = request.AsActive,
        };
    }
}
