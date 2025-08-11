public interface IVehicle
{
    public void Move();
}

public interface IMotorVehicle : IVehicle
{
    public string StartEngine();
}

public class Car : IMotorVehicle
{
    public void Move()
    {
        Console.WriteLine($"{StartEngine()}, Car is moving");
    }

    public string StartEngine()
    {
        string result = "Engine Started";
        return result;
    }
}

public class Bicycle : IVehicle
{
    public void Move()
    {
        Console.WriteLine("Bicycle is moving");
    }
}