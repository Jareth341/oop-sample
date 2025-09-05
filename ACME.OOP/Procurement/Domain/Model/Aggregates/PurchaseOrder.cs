using System.Runtime.InteropServices;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// PurchaseOrder aggregate root representing a purchase order in the Procurement bounded context.
/// </summary>
/// <param name="orderNumber"> the unique identifier for the purchase order, must not be null or empty</param>
/// <param name="supplierId"> the identifier of the supplier from whom the order is placed, must not be null</param>
/// <param name="orderDate"> the date when the order was placed</param>
/// <param name="currency"> the currency code for the order, must be a valid 3-letter ISO currency code</param>
/// 
/// <exception cref="ArgumentNullException">Thrown when orderNumber or supplierId is null</exception>
/// <exception cref="ArgumentException">Thrown when currency is null, empty, or not a valid 3-letter ISO code</exception>

public class PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    private readonly List<PurchaseOrderItem> _items = new();
    
    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentNullException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    public string Currency { get; } = string. IsNullOrWhiteSpace(currency) || currency.Length != 3 ? throw new ArgumentNullException(nameof(currency)): currency;
    
    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();
    
    /// <summary>
    ///  Adds an item to the purchase order with the specified product ID, quantity, and unit price amount.
    /// </summary>
    /// <param name="productId"> the identifier of the product being ordered, must not be null</param>
    /// <param name="quantity"> the quantity of the product being ordered, must be greater than zero</param>
    /// <param name="unitpriceAmount"> the price per unit of the product, must be a positive value</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when quantity is less than or equal to zero or unitpriceAmount is negative</exception>

    public void AddItem(ProductId productId, int quantity, decimal unitpriceAmount)
    {
      ArgumentNullException.ThrowIfNull(productId);
      if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero");
      if (unitpriceAmount < 0) throw new ArgumentOutOfRangeException(nameof(unitpriceAmount), "Unit price must be a positive value");
      
        var unitprice = new Money(unitpriceAmount, Currency);
        var item = new PurchaseOrderItem(productId, quantity, unitprice);
        _items.Add(item);
        
    }
    
    /// <summary>
    /// calculates the total amount for the purchase order by summing the total of each item.
    /// </summary>
    /// <returns>The total amount as a Money value object.</returns>
    
    public Money CalculateOrderTotal()
    {
        var totalAmount = _items.Sum(item => item.CalculateItemTotal().Amount);
        return new Money(totalAmount, Currency);
    }
}