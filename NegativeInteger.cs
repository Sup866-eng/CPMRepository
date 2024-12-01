using System;

public class NegativeInteger
{
    private int _number;
    public NegativeInteger(int number)
    {
        if (number >= 0)
        {
            throw new ArgumentException("Число должно быть отрицательным.");
        }
        _number = number;
        Console.WriteLine("Создан экземпляр класса NegativeInteger.");
    }
    public NegativeInteger(NegativeInteger other)
    {
        _number = other._number;
        Console.WriteLine("Создана копия экземпляра класса NegativeInteger.");
    }
    public int GetNumber()
    {
        return _number;
    }
    public void SetNumber(int number)
    {
        if (number < 0)
        {
            _number = number;
            Console.WriteLine($"Значение установлено на: {number}");
        }
        else
        {
            Console.WriteLine("Ошибка: значение должно быть отрицательным.");
        }
    }
    public void DisplayNumber()
    {
        Console.WriteLine($"Текущее значение: {_number}");
    }
    public int CompareWith(NegativeInteger other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Переданный объект не должен быть null.");
        }
        return _number.CompareTo(other._number);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            NegativeInteger negInt1 = new NegativeInteger(-15);
            negInt1.DisplayNumber();
            negInt1.SetNumber(-25);
            negInt1.DisplayNumber();
            Console.WriteLine($"Текущее значение объекта: {negInt1.GetNumber()}");
            NegativeInteger negInt2 = new NegativeInteger(negInt1);
            negInt2.DisplayNumber();
            int comparisonResult = negInt1.CompareWith(negInt2);
            switch (comparisonResult)
            {
                case > 0:
                    Console.WriteLine("Первый объект больше второго.");
                    break;
                case < 0:
                    Console.WriteLine("Первый объект меньше второго.");
                    break;
                default:
                    Console.WriteLine("Оба объекта равны.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
