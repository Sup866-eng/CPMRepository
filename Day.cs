using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Day
    {
        public string Name {  get; set; }
        public int numberOfClasses {  get; set; }
        public Day(string name, int num) {
            this.Name = name;
            this.numberOfClasses = num;
        }
    }
}
