
public class OrderProcessor
{
    IEmailSender _emailSender;
    ICreditCardProcessor _creditCardProcessor;
    IDatabaseInventory _databaseInventory;

    public OrderProcessor(IEmailSender emailSender, ICreditCardProcessor creditCardProcessor, IDatabaseInventory databaseInventory)
    {
        _emailSender = emailSender;
        _creditCardProcessor = creditCardProcessor;
        _databaseInventory = databaseInventory;
    }

    
    public void ProcessOrder(Order order)
    {
        _creditCardProcessor.ProcessPayment(order.Total);
        _databaseInventory.UpdateStock(order.Items);
        _emailSender.SendConfirmation(order.CustomerEmail);
    }
}

public class DatabaseInventory : IDatabaseInventory
{
    public DatabaseInventory()
    {
    }

    public void UpdateStock(object items)
    {
        Console.WriteLine($"Stock item updated: {items.ToString()}");
    }
}

public class CreditCardProcessor : ICreditCardProcessor
{
    public CreditCardProcessor()
    {
    }

    public void ProcessPayment(object total)
    {
        Console.WriteLine($"Total payment processed: {total.ToString()}");
    }
}

public class EmailSender : IEmailSender
{
    public EmailSender()
    {
    }

    public void SendConfirmation(object customerEmail)
    {
        Console.WriteLine($"Sending confirmation to: {customerEmail.ToString()}");
    }

}

public interface IDatabaseInventory
{
    public void UpdateStock(object items);
}

public interface ICreditCardProcessor
{
    public void ProcessPayment(object total);
}

public interface IEmailSender
{
    public void SendConfirmation(object customerEmail);
}


public class Order
{
    public object Total { get; internal set; }
    public object Items { get; internal set; }
    public object CustomerEmail { get; internal set; }
}