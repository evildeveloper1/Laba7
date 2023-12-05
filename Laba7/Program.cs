using System;

class Calculator<T>
{
    public delegate T OperationDelegate(T x, T y);

    public OperationDelegate Add { get; set; }
    public OperationDelegate Subtract { get; set; }
    public OperationDelegate Multiply { get; set; }
    public OperationDelegate Divide { get; set; }

    public T PerformOperation(T x, T y, OperationDelegate operation)
    {
        if (operation == null)
        {
            throw new ArgumentException("Operation not defined");
        }

        return operation(x, y);
    }
}

class Program
{
    static void Main()
    {
        Calculator<int> intCalculator = new Calculator<int>
        {
            Add = (x, y) => x + y,
            Subtract = (x, y) => x - y,
            Multiply = (x, y) => x * y,
            Divide = (x, y) => x / y
        };

        Console.WriteLine($"Addition: {intCalculator.PerformOperation(5, 3, intCalculator.Add)}");
        Console.WriteLine($"Subtraction: {intCalculator.PerformOperation(5, 3, intCalculator.Subtract)}");
        Console.WriteLine($"Multiplication: {intCalculator.PerformOperation(5, 3, intCalculator.Multiply)}");
        Console.WriteLine($"Division: {intCalculator.PerformOperation(5, 3, intCalculator.Divide)}");

        Calculator<double> doubleCalculator = new Calculator<double>
        {
            Add = (x, y) => x + y,
            Subtract = (x, y) => x - y,
            Multiply = (x, y) => x * y,
            Divide = (x, y) => x / y
        };

        Console.WriteLine($"Addition: {doubleCalculator.PerformOperation(5.5, 3.2, doubleCalculator.Add)}");
        Console.WriteLine($"Subtraction: {doubleCalculator.PerformOperation(5.5, 3.2, doubleCalculator.Subtract)}");
        Console.WriteLine($"Multiplication: {doubleCalculator.PerformOperation(5.5, 3.2, doubleCalculator.Multiply)}");
        Console.WriteLine($"Division: {doubleCalculator.PerformOperation(5.5, 3.2, doubleCalculator.Divide)}");

        Console.ReadLine();
    }
}
