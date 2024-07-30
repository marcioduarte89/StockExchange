using Order.Messages.Common;

namespace Order.Messages.Events;

public record OrderCreated
{
    public long Id { get; init; }

    public long StockId { get; init; }

    public long CustomerId { get; init; }
    
    public OrderType OrderType { get; init; }
    
    public OrderClass OrderClass { get; init; }
    
    public decimal UnitPrice { get; init; }
    
    public int NumberOfShares { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime? LastModifiedAt { get; init; }
}