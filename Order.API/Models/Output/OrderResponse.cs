using Order.Domain;

namespace Order.API.Models.Output;

public record OrderResponse(long OrderId, Guid OrderUId, long CustomerId, long StockId, OrderType OrderType, OrderClass OrderClass, decimal UnitPrice, int NumberOfShares);