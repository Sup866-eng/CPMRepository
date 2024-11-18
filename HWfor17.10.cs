using System;

namespace HWfor17._10
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Enter first number from 0 to 255: ");
                byte firstNumber = byte.Parse(Console.ReadLine());
                Console.Write("Enter second number from 0 to 255: ");
                byte secondNumber = byte.Parse(Console.ReadLine());
                double result = (double) firstNumber/secondNumber;
                if(secondNumber == 0)
                {
                    throw new DivideByZeroException();
                }
                Console.WriteLine($"Division result: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: you've entered not a number");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: the number is bigger than byte.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: second number is 0.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}.");
            }
        }
    }
}
