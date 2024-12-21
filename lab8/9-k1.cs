abstract class Command
{
    protected ArithmeticUnit unit;
    protected double operand;
    public abstract void Execute();
    public abstract void UnExecute();
}

class ArithmeticUnit
{
    public double Register { get; private set; }
    public void Run(char operationCode, double operand)
    {
        switch (operationCode)
        {
            case '+':
                Register += operand;
                break;
            case '-':
                Register -= operand;
                break;
            case '*':
                Register *= operand;
                break;
            case '/':
                Register /= operand;
                break;
        }
    }
}

class ControlUnit
{
    private List<Command> commands = new List<Command>();
    private int current = 0;

    public void StoreCommand(Command command)
    {
        if (current < commands.Count)
        {
            commands.RemoveRange(current, commands.Count - current);
        }
        commands.Add(command);
    }

    public void ExecuteCommand()
    {
        commands[current].Execute();
        current++;
    }

    public void Undo()
    {
        if (current > 0)
        {
            commands[current - 1].UnExecute();
            current--;
        }
    }

    public void Redo()
    {
        if (current < commands.Count)
        {
            commands[current].Execute();
            current++;
        }
    }

    // Многоуровневое Undo
    public void Undo(int levels)
    {
        for (int i = 0; i < levels && current > 0; i++)
        {
            Undo();
        }
    }

    // Многоуровневое Redo
    public void Redo(int levels)
    {
        for (int i = 0; i < levels && current < commands.Count; i++)
        {
            Redo();
        }
    }
}
class Add : Command
{
    public Add(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('+', operand);
    }

    public override void UnExecute()
    {
        unit.Run('-', operand);
    }
}

class Subtract : Command
{
    public Subtract(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('-', operand);
    }

    public override void UnExecute()
    {
        unit.Run('+', operand);
    }
}

class Multiply : Command
{
    public Multiply(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('*', operand);
    }

    public override void UnExecute()
    {
        unit.Run('/', operand);
    }
}

class Divide : Command
{
    public Divide(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('/', operand);
    }

    public override void UnExecute()
    {
        unit.Run('*', operand);
    }
}

class Calculator
{
    ArithmeticUnit arithmeticUnit;
    ControlUnit controlUnit;

    public Calculator()
    {
        arithmeticUnit = new ArithmeticUnit();
        controlUnit = new ControlUnit();
    }

    private double Run(Command command)
    {
        controlUnit.StoreCommand(command);
        controlUnit.ExecuteCommand();
        return arithmeticUnit.Register;
    }

    public double Add(double operand)
    {
        return Run(new Add(arithmeticUnit, operand));
    }

    public double Subtract(double operand)
    {
        return Run(new Subtract(arithmeticUnit, operand));
    }

    public double Multiply(double operand)
    {
        return Run(new Multiply(arithmeticUnit, operand));
    }

    public double Divide(double operand)
    {
        return Run(new Divide(arithmeticUnit, operand));
    }

    public double Undo(int levels = 1)
    {
        controlUnit.Undo(levels);
        return arithmeticUnit.Register;
    }
    public double Redo(int levels = 1)
    {
        controlUnit.Redo(levels);
        return arithmeticUnit.Register;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var calculator = new Calculator();

        double result = calculator.Add(10);
        Console.WriteLine("Result after adding 10: " + result);

        result = calculator.Subtract(5);
        Console.WriteLine("Result after subtracting 5: " + result);

        result = calculator.Multiply(2);
        Console.WriteLine("Result after multiplying by 2: " + result);

        result = calculator.Divide(3);
        Console.WriteLine("Result after dividing by 3: " + result);

        result = calculator.Undo(2);
        Console.WriteLine("Result after Undoing 2 operations: " + result);

        result = calculator.Redo(1);
        Console.WriteLine("Result after Redoing 1 operation: " + result);
    }
}