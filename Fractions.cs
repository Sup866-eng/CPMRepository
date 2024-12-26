internal class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(2, 3);
        Console.WriteLine(f1 * f2); // 2/6
    }
}

public class Fraction
    {
        // Числитель дроби
        public int Numerator { get; set; }
        // Знаменатель дроби
        public int Denominator { get; set; }
    
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    
        public static Fraction operator *(Fraction fr1, Fraction fr2)
        {
            return new Fraction(fr1.Numerator * fr2.Numerator, fr1.Denominator * fr2.Denominator);
        } 
    }
