
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class ShippingMethod
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ShippingmethodId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? FlatRate { get; set; } 
 public virtual int? EstimatedDays { get; set; } 
 public virtual bool? AsActive { get; set; } 
public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
public virtual CarrierService? CarrierService { get; set; } 
 public virtual ShippingMethodType? MethodType { get; set; } 

    public static ShippingMethod FromRequest(ShippingMethodRequest request) {
        return new ShippingMethod {
            Id = request.Id,
            Name = request.Name,
            FlatRate = request.FlatRate,
            EstimatedDays = request.EstimatedDays,
            AsActive = request.AsActive,
            MethodType = request.MethodType,
        };
    }
}
