
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class CarrierService
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CarrierserviceId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
public virtual ICollection<ShippingMethod> ShippingMethods { get; set; } = new List<ShippingMethod>();
 public virtual Carrier? Carrier { get; set; } 
 public virtual ServiceLevel? ServiceLevel { get; set; } 

    public static CarrierService FromRequest(CarrierServiceRequest request) {
        return new CarrierService {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            Carrier = request.Carrier,
            ServiceLevel = request.ServiceLevel,
        };
    }
}
