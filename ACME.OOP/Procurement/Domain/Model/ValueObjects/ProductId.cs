namespace ACME.OOP.Procurement.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing a Product Identifier in the Procurement bounded context.
/// </summary>
public record ProductId
{
    public Guid Id { get; init; }
    
    /// <summary>
    /// Creates a new instance of the <see cref="ProductId"/>  .
    /// </summary>
    /// <param name="id">The </param>
    /// <exception cref="ArgumentException"></exception>

    public ProductId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Product id cannot be empty", nameof(id));
        Id = id;
    }
    /// <summary>
    /// Generates a new unique ProductId.
    /// </summary>
    /// <returns>A new instance </returns>
    public static ProductId New() => new (Guid.NewGuid());
    /// <summary>
    /// to string override to return the string representation of the ProductId.
    /// </summary>
    /// <returns> </returns>
    public override string ToString() => Id.ToString();
}