using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Faculty : IComparable<Faculty>
    {
        public string Name { get; set; }
        public int Hours { get; set; }
        public Teacher Teacher { get; set; }

        public int Complex { get; set; }

        public Faculty(string name, int complex)
        {
            this.Name = name;
            this.Hours = 0;
            this.Teacher = null;
            this.Complex = complex;
        }
        public Faculty(string name, int hours, int complex)
        {
            this.Name = name;
            this.Hours = hours;
            this.Teacher = null;
            this.Complex = complex;
        }
        public Faculty(string name, int hours, Teacher teacher, int complex)
        {
            this.Name = name;
            this.Hours = hours;
            this.Teacher = teacher;
            this.Complex = complex;
        }

        public int CompareTo(Faculty other)
        {
            if (Complex == other.Complex) return 0;
            if (Complex < other.Complex) return -1;
            return 1;
        }
    }
}