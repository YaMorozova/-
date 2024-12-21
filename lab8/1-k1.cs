// Интерфейс температурного датчика
interface ITemperatureSensor
{
    double GetTemperature(); // В градусах Цельсия
}

// Готовый класс стороннего датчика, возвращающий температуру в Фаренгейтах
class FahrenheitSensor
{
    public double GetFahrenheitTemperature()
    {
        System.Random random = new System.Random();
        return random.Next(-40, 121);
    }
}

// Адаптер для преобразования температуры из Фаренгейта в Цельсий
class Adapter : ITemperatureSensor
{
    private readonly FahrenheitSensor _fahrenheitSensor;

    public Adapter(FahrenheitSensor fahrenheitSensor)
    {
        _fahrenheitSensor = fahrenheitSensor;
    }

    public double GetTemperature()
    {
        // Получаем температуру в Фаренгейтах
        double fahrenheitTemperature = _fahrenheitSensor.GetFahrenheitTemperature();

        // Преобразуем в Цельсии
        return (fahrenheitTemperature - 32) * 5 / 9;
    }
}

// Класс системы климат-контроля
class ClimateControlSystem
{
    private readonly ITemperatureSensor _temperatureSensor;

    public ClimateControlSystem(ITemperatureSensor temperatureSensor)
    {
        _temperatureSensor = temperatureSensor;
    }

    public void MonitorTemperature()
    {
        // Получаем температуру и выводим ее
        double temperature = _temperatureSensor.GetTemperature();
        System.Console.WriteLine($"Текущая температура: {temperature:F2}°C");

        // Проверка температурных условий
        if (temperature < 18)
        {
            System.Console.WriteLine("Включаем отопление.");
        }
        else if (temperature > 26)
        {
            System.Console.WriteLine("Включаем охлаждение.");
        }
        else
        {
            System.Console.WriteLine("Температура в норме.");
        }
    }
}

// Основной класс программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем сторонний датчик
        FahrenheitSensor fahrenheitSensor = new FahrenheitSensor();

        // Создаем адаптер для преобразования в шкалу Цельсия
        ITemperatureSensor sensorAdapter = new Adapter(fahrenheitSensor);

        // Создаем систему климат-контроля
        ClimateControlSystem climateControl = new ClimateControlSystem(sensorAdapter);

        // Мониторинг температуры
        climateControl.MonitorTemperature();

        // Ожидание завершения программы
        System.Console.ReadKey();
    }
}