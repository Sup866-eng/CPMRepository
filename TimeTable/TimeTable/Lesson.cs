using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Lesson
    {
        public TimeSlot Time { get; set; }
        public Faculty Faculty { get; set; }
        public Teacher Teacher { get; set; }

        public Lesson(TimeSlot time, Faculty faculty, Teacher teacher)
        {
            this.Time = time;
            this.Faculty = faculty;
            this.Teacher = teacher;
        }
    }
}