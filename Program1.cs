using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Program
    {
        public static void main(string[] args)
        {
            List<Day> days = new List<Day>();

            string name = "";
            int numberOfFaculties = 0;

            while(name != "0")
            {
                name = Console.ReadLine();
                numberOfFaculties = int.Parse(Console.ReadLine());
                days.Add(new Day(name, numberOfFaculties));
            }


        }
    }
}
