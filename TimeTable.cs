using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WeekDay : IEnumerable<string>
{
    private static readonly List<string> Days = new() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    public IEnumerator<string> GetEnumerator() => Days.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Contains(string day) => Days.Any(d => d.Equals(day, StringComparison.OrdinalIgnoreCase));
    public string GetFormattedDay(string day) => Days.FirstOrDefault(d => d.Equals(day, StringComparison.OrdinalIgnoreCase)) ?? day;
}

public class Lesson : IComparable<Lesson>
{
    public string Name { get; set; }
    public string Teacher { get; set; }
    public int Difficulty { get; set; } // Lesson difficulty from 1 to 10
    public TimeSpan Duration { get; set; }

    public Lesson(string name, string teacher, int difficulty, TimeSpan duration)
    {
        if (duration.TotalMinutes != 45)
            throw new ArgumentException("Lesson must be exactly 45 minutes long.");

        Name = name;
        Teacher = teacher;
        Difficulty = difficulty;
        Duration = duration;
    }

    public int CompareTo(Lesson other)
    {
        return other.Difficulty.CompareTo(this.Difficulty); // Sorting by descending difficulty
    }
}

public class LessonTime
{
    public string Time { get; set; }
    public LessonTime(string time)
    {
        Time = time;
    }
}

public class Schedule
{
    private Dictionary<string, List<(LessonTime, Lesson)>> schedule;
    private string filePath;

    public Schedule(string path)
    {
        filePath = path;
        schedule = new Dictionary<string, List<(LessonTime, Lesson)>>();
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        if (!File.Exists(filePath)) return;

        string currentDay = null;
        foreach (var line in File.ReadLines(filePath))
        {
            if (new WeekDay().Contains(line))
            {
                currentDay = new WeekDay().GetFormattedDay(line);
                schedule[currentDay] = new List<(LessonTime, Lesson)>();
            }
            else if (currentDay != null)
            {
                var parts = line.Split(' ');
                if (parts.Length >= 4 &&
                    int.TryParse(parts[2], out int difficulty) &&
                    TimeSpan.TryParse(parts[3], out TimeSpan duration))
                {
                    schedule[currentDay].Add((new LessonTime(parts[0]), new Lesson(parts[1], parts[2], difficulty, duration)));
                }
            }
        }
    }

    public void DisplaySchedule(string day)
    {
        day = new WeekDay().GetFormattedDay(day);
        if (schedule.ContainsKey(day))
        {
            Console.WriteLine($"Schedule for {day}:");
            foreach (var lesson in schedule[day])
            {
                Console.WriteLine($"{lesson.Item1.Time} - {lesson.Item2.Name} (Teacher: {lesson.Item2.Teacher}, Difficulty: {lesson.Item2.Difficulty})");
            }
        }
        else
        {
            Console.WriteLine($"No lessons scheduled for {day}.");
        }
    }

    public void EditSchedule()
    {
        Console.WriteLine("Enter the day of the week:");
        string day = Console.ReadLine();
        day = new WeekDay().GetFormattedDay(day);

        if (!new WeekDay().Contains(day))
        {
            Console.WriteLine("Invalid day of the week.");
            return;
        }

        DisplaySchedule(day);
        Console.WriteLine("Would you like to edit the schedule? (yes/no)");
        if (Console.ReadLine().ToLower() == "yes")
        {
            Console.WriteLine("Enter lesson time:");
            string time = Console.ReadLine();

            Console.WriteLine("Enter lesson name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter teacher's name:");
            string teacher = Console.ReadLine();

            Console.WriteLine("Enter lesson difficulty (1-10):");
            int difficulty = int.Parse(Console.ReadLine());

            try
            {
                var lesson = new Lesson(name, teacher, difficulty, TimeSpan.FromMinutes(45));
                AddOrUpdateLesson(day, time, lesson);
                Console.WriteLine("Schedule updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void AddOrUpdateLesson(string day, string time, Lesson lesson)
    {
        day = new WeekDay().GetFormattedDay(day);
        var lessonTime = new LessonTime(time);
        if (!schedule.ContainsKey(day))
            schedule[day] = new List<(LessonTime, Lesson)>();

        var existingLesson = schedule[day].FirstOrDefault(l => l.Item1.Time == time);
        if (existingLesson != default)
        {
            schedule[day].Remove(existingLesson);
        }
        schedule[day].Add((lessonTime, lesson));
        schedule[day].Sort((x, y) => x.Item2.CompareTo(y.Item2)); // Sorting by difficulty
        SaveToFile();
    }

    private void SaveToFile()
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var day in schedule.Keys)
            {
                writer.WriteLine(day);
                foreach (var lesson in schedule[day])
                {
                    writer.WriteLine($"{lesson.Item1.Time} {lesson.Item2.Name} {lesson.Item2.Teacher} {lesson.Item2.Difficulty} {lesson.Item2.Duration}");
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var schedule = new Schedule("schedule.txt");

        while (true)
        {
            Console.WriteLine("Would you like to view or edit the schedule? (view/edit/exit)");
            string command = Console.ReadLine().ToLower();

            if (command == "view")
            {
                Console.WriteLine("Enter the day of the week:");
                string day = Console.ReadLine();
                schedule.DisplaySchedule(day);
            }
            else if (command == "edit")
            {
                schedule.EditSchedule();
            }
            else if (command == "exit")
            {
                break;
            }
        }
    }
}
