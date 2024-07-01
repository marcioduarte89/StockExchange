namespace Order.Domain;

public class Order
{
    public Order(long customerId, long stockId, OrderType orderType, OrderClass orderClass, decimal unitPrice, int numberOfShares)
    {
        CustomerId = customerId;
        StockId = stockId;
        OrderType = orderType;
        OrderClass = orderClass;
        UnitPrice = unitPrice;
        NumberOfShares = numberOfShares;
    }
    
    public long Id { get; set; }

    public long StockId { get; private set; }

    public long CustomerId { get; private set; }
    
    public OrderType OrderType { get; private set; }
    
    public OrderClass OrderClass { get; private set; }
    
    public decimal UnitPrice { get; private set; }
    
    public int NumberOfShares { get; private set; }
}