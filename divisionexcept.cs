using System;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            fir = Console.ReadLine();
            byte first;
            if (byte.TryParse(fir), out first) == false)
            {
                Console.WriteLine("It isn't a number between 0 and 255");
            }
            else
            {
                first = byte.Parse(Console.ReadLine());
            }
            sec = Console.Readline();
            byte second;
            if (byte.TryParse(sec, out second) == false)
            {
                Console.WriteLine("It isn't a number between 0 and 255");
            }
            else
            {
                second = byte.Parse(Console.ReadLine());
            }
            double div = first / second;
            Console.WriteLine(div);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Second number must'n be a zero!");
        }
    }
}