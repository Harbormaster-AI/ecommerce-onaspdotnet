
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class PaymentProvider
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentproviderId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual bool? Enabled { get; set; } 
 public virtual string? MerchantAccountId { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
 public virtual PaymentProviderType? ProviderType { get; set; } 

    public static PaymentProvider FromRequest(PaymentProviderRequest request) {
        return new PaymentProvider {
            Id = request.Id,
            Name = request.Name,
            Enabled = request.Enabled,
            MerchantAccountId = request.MerchantAccountId,
            ProviderType = request.ProviderType,
        };
    }
}
