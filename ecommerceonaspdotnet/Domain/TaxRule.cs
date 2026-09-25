
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class TaxRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TaxruleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual string? Region { get; set; } 
 public virtual Percentage? Rate { get; set; } 
 public virtual bool? TaxInclusive { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
 public virtual TaxClass? TaxClass { get; set; } 

    public static TaxRule FromRequest(TaxRuleRequest request) {
        return new TaxRule {
            Id = request.Id,
            Name = request.Name,
            Country = request.Country,
            Region = request.Region,
            Rate = request.Rate,
            TaxInclusive = request.TaxInclusive,
            TaxClass = request.TaxClass,
        };
    }
}
