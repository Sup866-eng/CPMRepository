using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string filename = "schedule.txt";

            Schedule schedule = new Schedule();
            schedule.LoadFromFile(filename);

            Console.WriteLine("Текущее расписание:");
            schedule.Display();

            // Пример: добавление нового урока
            var teacher = new Teacher("Кузнецова");
            var timeSlot = new TimeSlot("12:00");
            var faculty = new Faculty("История", 7);
            var lesson = new Lesson(timeSlot, faculty, teacher);
            var monday = schedule.GetDay("Понедельник");

            if (monday != null)
            {
                monday.AddLesson(lesson);
            }

            Console.WriteLine("\nРасписание после добавления урока:");
            schedule.Display();

            // Пример: удаление урока
            if (monday != null)
            {
                monday.RemoveLesson("9:00");
            }

            Console.WriteLine("\nРасписание после удаления урока:");
            schedule.Display();

            // Сохранение изменений в файл
            schedule.SaveToFile(filename);
        }
    }
}