public abstract class AutoBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double CostBase { get; set; }
    public abstract double GetCost();
    public override string ToString()
    {
        string s = String.Format("Ваш автомобиль: \n{0} \nОписание: {1} \nСтоимость {2}\n",
        Name, Description, GetCost());
        return s;
    }
}
class Renault : AutoBase
{
    public Renault(string name, string info, double costbase)
    {
        Name = name;
        Description = info;
        CostBase = costbase;
    }
    public override double GetCost()
    {
        return CostBase * 1.18;
    }
}
public class Toyota : AutoBase
{
    public Toyota(string name, string info, double costbase)
    {
        Name = name;
        Description = info;
        CostBase = costbase;
    }

    public override double GetCost()
    {
        return CostBase * 1.2; // Для Toyota установим другую наценку
    }
}
class DecoratorOptions : AutoBase
{
    protected AutoBase AutoProperty { get; set; }
    public string Title { get; set; }
    public DecoratorOptions(AutoBase au, string tit)
    {
        AutoProperty = au;
        Title = tit;
    }
    public override double GetCost()
    {
        return AutoProperty.GetCost(); // Декоратор делегирует вызов базовому объекту }
    }
}
class MediaNAV : DecoratorOptions
{
    public MediaNAV(AutoBase p, string t) : base(p, t)
    {
        AutoProperty = p;
        Name = p.Name + ". Современный";
        Description = p.Description + ". " + this.Title + ". Обновленная мультимедийная навигационная система";
    }
    public override double GetCost()
    {
        return AutoProperty.GetCost() + 15.99;
    }
}
class SystemSecurity : DecoratorOptions
{
    public SystemSecurity(AutoBase p, string t) : base(p, t)
    {
        AutoProperty = p;
        Name = p.Name + ". Повышенной безопасности";
        Description = p.Description + ". " + this.Title + ". Передние боковые подушки безопасности, ESP -система динамической стабилизации автомобиля";
    }
    public override double GetCost()
    {
        return AutoProperty.GetCost() + 20.99;
    }
}
class ClimateControl : DecoratorOptions
{
    public ClimateControl(AutoBase p) : base(p, "Климат-контроль")
    {
        Name = AutoProperty.Name + ". С климат-контролем";
        Description = AutoProperty.Description + ". Установлена система климат-контроля.";
    }

    public override double GetCost()
    {
        return AutoProperty.GetCost() + 25.99; // Цена за установку климат-контроля
    }
}

class ParkingSensors : DecoratorOptions
{
    public ParkingSensors(AutoBase p) : base(p, "Парктроники")
    {
        Name = AutoProperty.Name + ". С системой парктроников";
        Description = AutoProperty.Description + ". Установлена система парктроников.";
    }

    public override double GetCost()
    {
        return AutoProperty.GetCost() + 12.99; // Цена за установку парктроников
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Существующий автомобиль Renault
        Renault reno = new Renault("Рено", "Renault LOGAN Active", 499.0);
        Print(reno);

        // Новая модель автомобиля Toyota
        Toyota toyota = new Toyota("Toyota", "Toyota Corolla", 700.0);
        Print(toyota);

        // Добавляем мультимедийную навигацию и безопасность
        AutoBase renoWithNav = new MediaNAV(reno, "Навигация");
        Print(renoWithNav);

        AutoBase renoWithSecurity = new SystemSecurity(new MediaNAV(reno, "Навигация"), "Безопасность");
        Print(renoWithSecurity);

        // Добавляем функциональные возможности к Toyota
        AutoBase toyotaWithClimate = new ClimateControl(toyota);
        Print(toyotaWithClimate);

        AutoBase toyotaWithSensors = new ParkingSensors(toyota);
        Print(toyotaWithSensors);

        // Сочетание нескольких опций на автомобиле
        AutoBase fullyLoadedToyota = new ParkingSensors(new ClimateControl(toyota));
        Print(fullyLoadedToyota);
    }

    private static void Print(AutoBase av)
    {
        Console.WriteLine(av.ToString());
    }
}