using System;
namespace HWfor03._10
{
    public class Program
    {
        public void Main(string[] args)
        {
            int max = 500;
            for(byte i = 0; i<max; i++)
            {
                Console.WriteLine(i);
                if (i == byte.MaxValue)
                {
                    break;
                }
            }
        }
    }
}

//В исходном коде когда i достигает максимального значения своего типа данных, он спускается до минимального и тем самым никогда не доходит до значение max, цикл выполняется без конца;
