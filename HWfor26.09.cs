using System;

namespace HWfor26._09
{
    public class Program
    {
        public static void Main()
        { 
            Console.WriteLine("{0,-10} {1,-20} {2,30} {3,30}", "Type", "Byte(s) of memory", "Min", "Max");
            Console.WriteLine(new string('-', 100));
            PrintTypeInfo<sbyte>("sbyte");
            PrintTypeInfo<byte>("byte");
            PrintTypeInfo<short>("short");
            PrintTypeInfo<ushort>("ushort");
            PrintTypeInfo<int>("int");
            PrintTypeInfo<uint>("uint");
            PrintTypeInfo<long>("long");
            PrintTypeInfo<ulong>("ulong");
            PrintTypeInfo<float>("float");
            PrintTypeInfo<double>("double");
            PrintTypeInfo<decimal>("decimal");
        }

        static void PrintTypeInfo<T>(string typeName)
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            //честно не знаю что это за штука в строке выше, нашел в интернете, работает
            var minValue = typeof(T).GetField("MinValue").GetValue(null);
            var maxValue = typeof(T).GetField("MaxValue").GetValue(null);
            Console.WriteLine("{0,-10} {1,-20} {2,30} {3,30}", typeName, size, minValue, maxValue);
        }
    }
}
