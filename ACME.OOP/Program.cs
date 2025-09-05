using ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

var supplierAddress = new Address
(
    street: "123 Main St",
    number: "1213",
    city: "Metropolis",
    stateOrRegion: "NY",
    postalCode: "12345",
    country: "USA"
);
var supplier =new Supplier("SUP123", "Acme Supplies", supplierAddress);

var purchaseOrder = new PurchaseOrder("PO456", new SupplierId(supplier.Identifier), DateTime.UtcNow, "USD");

purchaseOrder.AddItem(ProductId.New(),10, 15.50m);
purchaseOrder.AddItem(ProductId.New(),5, 25.00m);

Console.WriteLine($"Order Total: {purchaseOrder.CalculateOrderTotal()}");