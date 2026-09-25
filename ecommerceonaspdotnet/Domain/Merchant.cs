
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Merchant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MerchantId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual string? DefaultCurrency { get; set; } 
 public virtual string? DefaultLocale { get; set; } 
 public virtual string? SupportEmail { get; set; } 
public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();
public virtual ICollection<FulfillmentCenter> FulfillmentCenters { get; set; } = new List<FulfillmentCenter>();
public virtual ICollection<TaxRule> TaxRules { get; set; } = new List<TaxRule>();
public virtual ICollection<PaymentProvider> PaymentProviders { get; set; } = new List<PaymentProvider>();
public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public static Merchant FromRequest(MerchantRequest request) {
        return new Merchant {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            Website = request.Website,
            DefaultCurrency = request.DefaultCurrency,
            DefaultLocale = request.DefaultLocale,
            SupportEmail = request.SupportEmail,
        };
    }
}
