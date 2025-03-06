using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Schedule
    {
        private Dictionary<string, Day> days;

        public Schedule()
        {
            days = new Dictionary<string, Day>();
        }

        public void AddDay(Day day)
        {
            days[day.Name] = day;
        }

        public Day GetDay(string dayName)
        {
            if (days.ContainsKey(dayName))
            {
                return days[dayName];
            }
            return null;
        }

        public void Display()
        {
            foreach (var day in days.Values)
            {
                day.Display();
            }
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var day in days.Values)
                {
                    writer.WriteLine(day.Name);
                    foreach (var lesson in day.Lessons)
                    {
                        writer.WriteLine($"{lesson.Time.Time} {lesson.Faculty} {lesson.Teacher.Name}");
                    }
                    writer.WriteLine();
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            string[] lines = File.ReadAllLines(filename);
            Day currentDay = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (IsDay(line))
                {
                    if (currentDay != null)
                    {
                        AddDay(currentDay);
                    }
                    currentDay = new Day(line);
                }
                else
                {
                    var parts = line.Split(new[] { ' ' }, 3);
                    var time = new TimeSlot(parts[0]);
                    var faculty = new Faculty(parts[1], 1);
                    var teacher = new Teacher(parts[2]);

                    if (currentDay != null)
                    {
                        currentDay.AddLesson(new Lesson(time, faculty, teacher));
                    }
                }
            }

            if (currentDay != null)
            {
                AddDay(currentDay);
            }
        }

        private bool IsDay(string line)
        {
            string[] daysOfWeek = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            return Array.Exists(daysOfWeek, day => day == line);
        }
    }
}
