abstract class CarFactory
{
    public abstract AbstractCar CreateCar();
    public abstract AbstractEngine CreateEngine();
}
abstract class AbstractCar
{
    public string Name { get; set; }
    public string BodyType { get; set; } // Новый параметр
    public abstract int MaxSpeed(AbstractEngine engine);
}
abstract class AbstractEngine
{
    public int max_speed { get; set; }
}
class FordFactory : CarFactory
{
    static FordFactory _instance = null;

    // Метод для получения экземпляра
    public static FordFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FordFactory();
            }
            return _instance;
        }
    }
    public override AbstractCar CreateCar()
    {
        return new FordCar("Форд");
    }
    public override AbstractEngine CreateEngine()
    {
        return new FordEngine();
    }
}
class FordCar : AbstractCar
{
    public FordCar(string name)
    {
        Name = name;
    }
    public override int MaxSpeed(AbstractEngine engine)
    {
        int ms = engine.max_speed;
        return ms;
    }
    public override string ToString()
    {
        return "Автомобиль " + Name;

    }
}
class FordEngine : AbstractEngine
{
    public FordEngine()
    {
        max_speed = 220;
    }
}
class AudiFactory : CarFactory
{
    public override AbstractCar CreateCar()
    {
        return new AudiCar("Audi", "Седан");
    }

    public override AbstractEngine CreateEngine()
    {
        return new AudiEngine();
    }
}

class AudiCar : AbstractCar
{
    public AudiCar(string name, string bodyType)
    {
        Name = name;
        BodyType = bodyType;
    }

    public override int MaxSpeed(AbstractEngine engine)
    {
        int ms = engine.max_speed;
        return ms;
    }

    public override string ToString()
    {
        return $"Автомобиль {Name}, кузов: {BodyType}";
    }
}

class AudiEngine : AbstractEngine
{
    public AudiEngine()
    {
        max_speed = 250;
    }
}
class Client
{
    private AbstractCar abstractCar;
    private AbstractEngine abstractEngine;
    public Client(CarFactory car_factory)
    {
        abstractCar = car_factory.CreateCar();
        abstractEngine = car_factory.CreateEngine();
    }
    public int RunMaxSpeed()
    {
        return abstractCar.MaxSpeed(abstractEngine);
    }
    public override string ToString()
    {
        return abstractCar.ToString();
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Использование фабрики через Singleton
        CarFactory ford_car = FordFactory.Instance;
        Client c1 = new Client(ford_car);
        Console.WriteLine("Максимальная скорость {0} составляет {1} км/час",
        c1.ToString(), c1.RunMaxSpeed());

        // Создаем клиента для Audi
        CarFactory audi_car = new AudiFactory();
        Client audiClient = new Client(audi_car);
        Console.WriteLine("Максимальная скорость {0} составляет {1} км/час", audiClient.ToString(), audiClient.RunMaxSpeed());
    }
}