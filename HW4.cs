using System;

class Program{
    static void Main(){
        Console.Write("Enter a positive integer: ");
        try{
            string input = Console.ReadLine();
            int number = int.Parse(input);
            string factors = GetFactors(number);
            Console.WriteLine($"Factors of a number {number}: {factors}");
        }
        catch (FormatException){
            Console.WriteLine("Error: It must be an integer.");
        }
        catch (OverflowException){
            Console.WriteLine("Error: This number is too big for an integer.");
        }
        catch (Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static string GetFactors(int number){
        if (number < 2){
            return "Нет простых множителей!";
        }
        string result = "";
        for (int i = 2; i <= number; i++){
            while (number % i == 0){
                result += i + " ";
                number /= i;
            }
        }
        return result;
    }
}
