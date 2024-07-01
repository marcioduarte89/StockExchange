using Order.Domain;

namespace Order.API.Models.Output;

public class OrderResponse(long CustomerId, long StockId, OrderType OrderType, OrderClass OrderClass, decimal UnitPrice, int NumberOfShares);