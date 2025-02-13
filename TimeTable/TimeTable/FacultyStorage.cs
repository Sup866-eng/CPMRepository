using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class FacultyStorage : IEnumerable<Faculty>
    {
        private readonly Faculty[] faculties;

        public FacultyStorage(Faculty[] faculties)
        {
            this.faculties = faculties;
        }

        public IEnumerator<Faculty> GetEnumerator()
        {
            return ((IEnumerable<Faculty>)faculties).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return faculties.GetEnumerator();
        }
    }
}
