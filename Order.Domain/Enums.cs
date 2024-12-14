namespace Order.Domain;

public enum OrderType : byte
{
    Buy,
    Sell
}

public enum OrderClass : byte
{
    Market
}