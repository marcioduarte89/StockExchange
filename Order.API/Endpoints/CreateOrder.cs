using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.API.Mappings;
using Order.API.Models.Output;
using Order.Domain;
using Order.Infrastructure;
using Input = Order.API.Models.Input;

namespace Order.API.Endpoints;

public class CreateOrder : Endpoint<Input.CreateOrder, Results<Accepted<OrderResponse>, NotFound, ProblemDetails>, CreateOrderToOrderMapping>
{
    private OrderDbContext _dbContext;

    public CreateOrder(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public override void Configure()
    {
        Post("/orders");
        AllowAnonymous();
    }

    public override async Task<Results<Accepted<OrderResponse>, NotFound, ProblemDetails>> ExecuteAsync(Input.CreateOrder req, CancellationToken ct)
    {
        var order = Map.ToEntity(req);
        await _dbContext.Database.EnsureCreatedAsync(ct);

        _dbContext.Add(order);
        await _dbContext.SaveChangesAsync(ct);

        var response = Map.FromEntity(order);
        return TypedResults.Accepted("stuff", response);
    }
}