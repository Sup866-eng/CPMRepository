using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Faculty[] faculties = new Faculty[3];
            faculties[0] = new Faculty("Maths", 10, "Aidar Ildarovich", 8);
            faculties[1] = new Faculty("Physics", 12, "Artem Pivovarchik", 10);
            faculties[2] = new Faculty("Chemistry", 12, "Aleksei Mishurinskiy", 9);
            FacultyStorage facStorage = new FacultyStorage(faculties);
            Array.Sort(faculties, new FacultyComparer());
            foreach (var fac in faculties) {
                Console.WriteLine(fac.Name);
            }
            Day[] days = new Day[7];
            days[0] = new Day("Monday", 1);
            days[1] = new Day("Tuesday", 2);
            days[2] = new Day("Wednesday", 3);
            days[3] = new Day("Thursday", 4);
            days[4] = new Day("Friday", 5);
            days[5] = new Day("Saturday", 6);
            days[6] = new Day("Sunday", 7);
            DaysStorage _days = new DaysStorage(days);
            foreach (var day in days)
            {
                Console.WriteLine(day.Name);
            }
        }
    }
}
