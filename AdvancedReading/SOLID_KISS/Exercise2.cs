public interface IDiscount
{
    public decimal ApplyDiscount(decimal amount);
}

public class RegularDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * 0.95m;
    }
}

public class VIPDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * 0.9m;
    }
}

public class PremiumDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * 0.8m;
    }
}

public class NoDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount;
    }
}

public class DiscountCalculator
{
    private readonly Dictionary<string, IDiscount> _strategies;

    public DiscountCalculator()
    {
        _strategies = new Dictionary<string, IDiscount>
        {
            { "Regular", new RegularDiscount() },
            { "VIP", new VIPDiscount() },
            { "Premium", new PremiumDiscount() }
        };
    }

    public decimal Calculate(decimal amount, string customerType)
    {
        if (_strategies.TryGetValue(customerType, out var strategy))
        {
            var discounted = strategy.ApplyDiscount(amount);
            return discounted;
        }

        var justAmount = new NoDiscount().ApplyDiscount(amount);
        return justAmount;
    }
}