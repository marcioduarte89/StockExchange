using Order.Domain;

namespace Order.API.Models.Input;

public record CreateOrder(long CustomerId, long StockId, OrderType OrderType, OrderClass OrderClass, decimal UnitPrice, int NumberOfShares);
