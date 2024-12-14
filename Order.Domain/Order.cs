namespace Order.Domain;

public class Order
{
    public Order(long customerId, long stockId, OrderType orderType, OrderClass orderClass, decimal unitPrice, int numberOfShares)
    {
        CustomerId = customerId;
        UId = Guid.NewGuid();
        StockId = stockId;
        OrderType = orderType;
        OrderClass = orderClass;
        UnitPrice = unitPrice;
        NumberOfShares = numberOfShares;
        CreatedAt = DateTime.UtcNow;
    }
    
    public long Id { get; private set; }
    
    public Guid UId { get; private set; }

    public long StockId { get; private set; }

    public long CustomerId { get; private set; }
    
    public OrderType OrderType { get; private set; }
    
    public OrderClass OrderClass { get; private set; }
    
    public decimal UnitPrice { get; private set; }
    
    public int NumberOfShares { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime? LastModifiedAt { get; private set; }
}