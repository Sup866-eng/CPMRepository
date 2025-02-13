using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class DayStorageEnumerator:IEnumerator<Day>
    {
        private readonly Day[] days;
        private int currentIndex = -1;
        public DayStorageEnumerator(Day[] days)
        {
            this.days = days;
        }

        public Day Current => days[currentIndex];

        object IEnumerator.Current => days[currentIndex];

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            currentIndex++;
            if (currentIndex >= days.Length)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            currentIndex = -1;
        }
    }
}
