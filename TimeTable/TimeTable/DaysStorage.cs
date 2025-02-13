using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class DaysStorage: IEnumerable<Day>
    {
        private readonly Day[] _days;
        public DaysStorage(Day[] days)
        {
            _days = days;
        }

        public IEnumerator<Day> GetEnumerator()
        {
            return ((IEnumerable<Day>)_days).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _days.GetEnumerator();
        }
    }
}
