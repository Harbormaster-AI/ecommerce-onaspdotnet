
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Channel
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ChannelId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ChannelCode { get; set; } 
 public virtual string? Locale { get; set; } 
 public virtual string? Domain { get; set; } 
 public virtual bool? AsActive { get; set; } 
 public virtual string? DefaultCurrency { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>();
public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
public virtual ICollection<ShippingMethod> ShippingMethods { get; set; } = new List<ShippingMethod>();
public virtual ICollection<PaymentProvider> PaymentProviders { get; set; } = new List<PaymentProvider>();
 public virtual ChannelType? ChannelType { get; set; } 

    public static Channel FromRequest(ChannelRequest request) {
        return new Channel {
            Id = request.Id,
            Name = request.Name,
            ChannelCode = request.ChannelCode,
            Locale = request.Locale,
            Domain = request.Domain,
            AsActive = request.AsActive,
            DefaultCurrency = request.DefaultCurrency,
            ChannelType = request.ChannelType,
        };
    }
}
