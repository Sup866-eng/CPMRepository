using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class FacultyComparer:IComparer<Faculty>
    {
        public int Compare(Faculty fac1, Faculty fac2) { 
            return fac1.Complex.CompareTo(fac2.Complex);
        }
    }
}
