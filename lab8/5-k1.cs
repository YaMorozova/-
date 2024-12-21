interface IRouteStrategy
{
    void BuildRoute(string start, string end);
}
class RoadRouteStrategy : IRouteStrategy
{
    public void BuildRoute(string start, string end)
    {
        Console.WriteLine($"Прокладывается маршрут по автодорогам от {start} до {end}.");
    }
}

class WalkRouteStrategy : IRouteStrategy
{
    public void BuildRoute(string start, string end)
    {
        Console.WriteLine($"Прокладывается пеший маршрут от {start} до {end}.");
    }
}

class BicycleRouteStrategy : IRouteStrategy
{
    public void BuildRoute(string start, string end)
    {
        Console.WriteLine($"Прокладывается маршрут по велодорожкам от {start} до {end}.");
    }
}

class PublicTransportRouteStrategy : IRouteStrategy
{
    public void BuildRoute(string start, string end)
    {
        Console.WriteLine($"Прокладывается маршрут на общественном транспорте от {start} до {end}.");
    }
}

class TouristAttractionRouteStrategy : IRouteStrategy
{
    public void BuildRoute(string start, string end)
    {
        Console.WriteLine($"Прокладывается маршрут посещения достопримечательностей от {start} до {end}.");
    }
}
class Navigator
{
    private IRouteStrategy _routeStrategy;

    // Установка стратегии маршрута
    public void SetRouteStrategy(IRouteStrategy routeStrategy)
    {
        _routeStrategy = routeStrategy;
    }

    // Построение маршрута с использованием выбранной стратегии
    public void BuildRoute(string start, string end)
    {
        if (_routeStrategy == null)
        {
            Console.WriteLine("Стратегия маршрута не выбрана.");
            return;
        }

        _routeStrategy.BuildRoute(start, end);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Navigator navigator = new Navigator();

        // Прокладка маршрута по автодорогам
        navigator.SetRouteStrategy(new RoadRouteStrategy());
        navigator.BuildRoute("Москва", "Санкт-Петербург");

        // Прокладка пешего маршрута
        navigator.SetRouteStrategy(new WalkRouteStrategy());
        navigator.BuildRoute("Дом", "Метро");

        // Прокладка маршрута по велодорожкам
        navigator.SetRouteStrategy(new BicycleRouteStrategy());
        navigator.BuildRoute("Дача", "Озеро");

        // Прокладка маршрута на общественном транспорте
        navigator.SetRouteStrategy(new PublicTransportRouteStrategy());
        navigator.BuildRoute("Дом", "ИТМО");

        // Прокладка маршрута посещения достопримечательностей
        navigator.SetRouteStrategy(new TouristAttractionRouteStrategy());
        navigator.BuildRoute("Эрмитаж", "Медный всадник");
    }
}