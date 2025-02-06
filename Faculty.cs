using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Faculty
    {
        public string Name { get; set; }
        public int Hours { get; set; }
        public string Teacher { get; set; }

        public Faculty(string name)
        {
            this.Name = name;
            this.Hours = 0;
            this.Teacher = null;
        }
        public Faculty(string name, int hours)
        {
            this.Name = name;
            this.Hours = hours;
            this.Teacher = null;
        }
        public Faculty(string name, int hours, string teacher)
        {
            this.Name = name;
            this.Hours = hours;
            this.Teacher = teacher;
        }
    }
}
