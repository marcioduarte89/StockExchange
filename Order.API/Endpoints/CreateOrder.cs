using System.Transactions;
using FastEndpoints;
using Hangfire;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.API.Jobs;
using Order.API.Mappings;
using Order.API.Models.Output;
using Order.Infrastructure;
using Input = Order.API.Models.Input;

namespace Order.API.Endpoints;

public class CreateOrder : Endpoint<
    Input.CreateOrder,
    Results<Accepted<OrderResponse>, NotFound, ProblemDetails>,
    CreateOrderToOrderMapping>
{
    private IOrderDbContext _dbContext;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public CreateOrder(IOrderDbContext dbContext, IBackgroundJobClient backgroundJobClient)
    {
        _dbContext = dbContext;
        _backgroundJobClient = backgroundJobClient;
    }

    public override void Configure()
    {
        Post("/orders");
        AllowAnonymous();
    }

    public override async Task<Results<Accepted<OrderResponse>, NotFound, ProblemDetails>> ExecuteAsync(
        Input.CreateOrder req, CancellationToken ct)
    {
        var order = Map.ToEntity(req);
        // await _dbContext.Database.EnsureDeletedAsync(ct);
        // await _dbContext.Database.EnsureCreatedAsync(ct);

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled); 
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(ct);
        var response = Map.FromEntity(order);
        _backgroundJobClient.Enqueue<EnqueueOrderJob>((x) => x.Execute(response, ct));
        scope.Complete();

        
        return TypedResults.Accepted("stuff", response);
    }
}