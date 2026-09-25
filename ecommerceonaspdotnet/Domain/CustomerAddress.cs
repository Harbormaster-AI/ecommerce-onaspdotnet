
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class CustomerAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CustomeraddressId { get; set; } 
 public virtual string? Label { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual bool? AsDefaultShipping { get; set; } 
 public virtual bool? AsDefaultBilling { get; set; } 
public virtual Customer? Customer { get; set; } 

    public static CustomerAddress FromRequest(CustomerAddressRequest request) {
        return new CustomerAddress {
            Id = request.Id,
            Label = request.Label,
            Address = request.Address,
            AsDefaultShipping = request.AsDefaultShipping,
            AsDefaultBilling = request.AsDefaultBilling,
        };
    }
}
