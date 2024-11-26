using System.Numerics;

namespace WebProject;
public partial class Calculator
{
    public int Add(int a, int b)
    {
        var result = a + b;
        Console.WriteLine($"The result of adding {a} and {b} is {result}");
        return result;
    }

    public int Subtract(int a, int b)
    {
        var result = a - b;
        if(result < 0)
        {
            Console.WriteLine("Result of subtraction is negative");
        }
        return result; 
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public int Divide(int a, int b)
    {
        if(b == 0)
        {
            throw new DivideByZeroException();
        }
        return a / b;
    }
}
