using FastEndpoints;
using Order.API.Models.Input;
using Order.API.Models.Output;

namespace Order.API.Mappings;

public class CreateOrderToOrderMapping: Mapper<CreateOrder, OrderResponse, Domain.Order>
{
    public override Domain.Order ToEntity(CreateOrder o)
    {
        return new Domain.Order(o.CustomerId, o.StockId, o.OrderType, o.OrderClass, o.UnitPrice, o.NumberOfShares);
    }

    public override OrderResponse FromEntity(Domain.Order o)
    {
        return new OrderResponse(o.CustomerId, o.StockId, o.OrderType, o.OrderClass, o.UnitPrice, o.NumberOfShares);
    }
}