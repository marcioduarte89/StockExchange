using FastEndpoints;
using Order.API.Models.Input;
using Order.API.Models.Output;
using Order.Domain;
using Order.Messages.Events;

namespace Order.API.Mappings;

public class CreateOrderToOrderMapping: Mapper<CreateOrder, OrderResponse, Domain.Order>
{
    public override Domain.Order ToEntity(CreateOrder o)
    {
        return new Domain.Order(o.CustomerId, o.StockId, o.OrderType, o.OrderClass, o.UnitPrice, o.NumberOfShares);
    }

    public override OrderResponse FromEntity(Domain.Order o)
    {
        return new OrderResponse(o.Id, o.UId, o.CustomerId, o.StockId, o.OrderType, o.OrderClass, o.UnitPrice, o.NumberOfShares);
    }
    
    public static OrderCreated FromEntity(OrderResponse o)
    { 
        return new OrderCreated
        {
            Id = o.OrderId,
            StockId = o.StockId,
            CustomerId = o.CustomerId,
            OrderType = ToEventOrderType(o.OrderType),
            OrderClass = ToEventOrderClass(o.OrderClass),
            UnitPrice = o.UnitPrice,
            NumberOfShares = o.NumberOfShares,
        };
    }
    
    private static Messages.Common.OrderType ToEventOrderType(OrderType orderType)
    {
        return orderType switch
        {
            OrderType.Buy => Messages.Common.OrderType.Buy,
            OrderType.Sell => Messages.Common.OrderType.Sell,
            _ => throw new ArgumentOutOfRangeException(nameof(orderType), orderType, null)
        };
    }
    
    private static Messages.Common.OrderClass ToEventOrderClass(OrderClass orderClass)
    {
        return orderClass switch
        {
            OrderClass.Market => Messages.Common.OrderClass.Market,
            _ => throw new ArgumentOutOfRangeException(nameof(orderClass), orderClass, null)
        };
    }
}