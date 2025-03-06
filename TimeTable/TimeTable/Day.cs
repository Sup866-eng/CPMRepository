using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Day
    {
        public string Name { get; set; }
        public int numberOfClasses { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Day(string name)
        {
            Name = name;
            Lessons = new List<Lesson>();
        }

        public void AddLesson(Lesson lesson)
        {
            Lessons.Add(lesson);
        }

        public void RemoveLesson(string time)
        {
            Lessons.RemoveAll(l => l.Time.Time == time);
        }

        public void Display()
        {
            Console.WriteLine($"{Name}:");
            foreach (var lesson in Lessons)
            {
                Console.WriteLine($"  {lesson.Time.Time} - {lesson.Faculty} ({lesson.Teacher.Name})");
            }
            Console.WriteLine();
        }
    }
}