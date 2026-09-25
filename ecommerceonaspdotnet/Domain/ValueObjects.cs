using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceonaspdotnet.Domain;


    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record SKU(
    string Value
    );

    [ComplexType]
    public record Percentage(
    decimal Value
    );

