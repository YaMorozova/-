abstract class HaircutProcess
{
    // Шаблонный метод
    public void PerformHaircut()
    {
        GreetClient(); // Общий шаг
        PrepareClient(); // Общий шаг
        CutHair(); // Изменчивый шаг
        StyleHair(); // Изменчивый шаг
        FinishProcess(); // Общий шаг
    }

    // Общие шаги для всех типов стрижек
    private void GreetClient()
    {
        Console.WriteLine("Добро пожаловать");
    }

    private void PrepareClient()
    {
        Console.WriteLine("Усаживаем клиента в кресло и надеваем защитный фартук.");
    }

    private void FinishProcess()
    {
        Console.WriteLine("Снимаем фартук.");
    }

    // Изменчивые шаги, реализуются в наследниках
    protected abstract void CutHair();
    protected abstract void StyleHair();
}
class MenHaircut : HaircutProcess
{
    protected override void CutHair()
    {
        Console.WriteLine("Выполняем короткую мужскую стрижку машинкой и ножницами.");
    }

    protected override void StyleHair()
    {
        Console.WriteLine("Укладываем волосы.");
    }
}
class WomenHaircut : HaircutProcess
{
    protected override void CutHair()
    {
        Console.WriteLine("Делаем женскую стрижку.");
    }

    protected override void StyleHair()
    {
        Console.WriteLine("Выполняем укладку феном.");
    }
}
class KidsHaircut : HaircutProcess
{
    protected override void CutHair()
    {
        Console.WriteLine("Аккуратно стрижем волосы, чтобы не испугать ребёнка.");
    }

    protected override void StyleHair()
    {
        Console.WriteLine("Причёсываем волосы расчёской.");
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Мужская стрижка
        HaircutProcess menHaircut = new MenHaircut();
        Console.WriteLine("Мужская стрижка");
        menHaircut.PerformHaircut();

        // Женская стрижка
        HaircutProcess womenHaircut = new WomenHaircut();
        Console.WriteLine("\nЖенская стрижка");
        womenHaircut.PerformHaircut();

        // Детская стрижка
        HaircutProcess kidsHaircut = new KidsHaircut();
        Console.WriteLine("\nДетская стрижка");
        kidsHaircut.PerformHaircut();
    }
}