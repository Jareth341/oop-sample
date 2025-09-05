using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents an item within a Purchase Order in the Procurement bounded context.
/// </summary>
/// <param name="productId">the identifier of the product being ordered, must not be null</param>
/// <param name="quantity">the quantity of the product being ordered, must be greater than zero</param>
/// <param name="unitprice">the price per unit of the product, must be a positive value</param>
///
/// 
/// <exception cref="ArgumentException">Thrown when productId is null or unitprice is null</exception>
/// <exception cref="ArgumentOutOfRangeException">Thrown when quantity is less than or equal

public class PurchaseOrderItem(ProductId productId, int quantity, Money unitprice )
{
    public ProductId ProductId { get; } = productId ?? throw new ArgumentException(nameof(productId));
    public int Quantity { get; } = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity));
    public Money UnitPrice { get; } = unitprice ?? throw new ArgumentException(nameof(unitprice), "Unit price cannot be null");
    
    /// <summary>
    /// Calculates the total price for this purchase order item by multiplying the unit price by the quantity.
    /// </summary>
    /// <returns>The total price as a Money value object.</returns>
    
    public Money CalculateItemTotal() => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}