using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// WeekDay class
public class WeekDay : IEnumerable<string>
{
    private static readonly List<string> Days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    public IEnumerator<string> GetEnumerator()
    {
        return Days.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(string day)
    {
        foreach (string d in Days)
        {
            if (d.Equals(day, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    public string GetFormattedDay(string day)
    {
        foreach (string d in Days)
        {
            if (d.Equals(day, StringComparison.OrdinalIgnoreCase))
            {
                return d;
            }
        }
        return day;
    }
}

// Lesson class
public class Lesson : IComparable<Lesson>
{
    public string Name { get; set; }
    public string Teacher { get; set; }
    public int Difficulty { get; set; } // Lesson difficulty from 1 to 10
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public Lesson(string name, string teacher, int difficulty, TimeSpan startTime)
    {
        Name = name;
        Teacher = teacher;
        Difficulty = difficulty;
        StartTime = startTime;
        EndTime = startTime.Add(TimeSpan.FromMinutes(45));
    }

    public int CompareTo(Lesson other)
    {
        return StartTime.CompareTo(other.StartTime);
    }
}

// Schedule class
public class Schedule
{
    private Dictionary<string, List<Lesson>> schedule;
    private string filePath;

    public Schedule(string path)
    {
        filePath = path;
        schedule = new Dictionary<string, List<Lesson>>();
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        if (!File.Exists(filePath)) return;

        string currentDay = null;
        foreach (var line in File.ReadLines(filePath))
        {
            WeekDay weekDay = new WeekDay();
            if (weekDay.Contains(line))
            {
                currentDay = weekDay.GetFormattedDay(line);
                schedule[currentDay] = new List<Lesson>();
            }
            else if (currentDay != null)
            {
                var parts = line.Split(' ');
                if (parts.Length >= 4 &&
                    int.TryParse(parts[2], out int difficulty) &&
                    TimeSpan.TryParse(parts[3], out TimeSpan startTime))
                {
                    schedule[currentDay].Add(new Lesson(parts[1], parts[2], difficulty, startTime));
                }
            }
        }
    }

    public void DisplaySchedule(string day)
    {
        WeekDay weekDay = new WeekDay();
        day = weekDay.GetFormattedDay(day);
        if (schedule.ContainsKey(day))
        {
            Console.WriteLine("Schedule for " + day + ":");
            foreach (var lesson in schedule[day])
            {
                Console.WriteLine(lesson.StartTime + " - " + lesson.EndTime + " " + lesson.Name + " (Teacher: " + lesson.Teacher + ", Difficulty: " + lesson.Difficulty + ")");
            }
        }
        else
        {
            Console.WriteLine("No lessons scheduled for " + day + ".");
        }
    }

    public void EditSchedule()
    {
        Console.WriteLine("Enter the day of the week:");
        string day = Console.ReadLine();

        WeekDay weekDay = new WeekDay();
        day = weekDay.GetFormattedDay(day);

        if (!weekDay.Contains(day))
        {
            Console.WriteLine("Invalid day of the week.");
            return;
        }

        DisplaySchedule(day);
        Console.WriteLine("Would you like to edit the schedule? (yes/no)");
        if (Console.ReadLine().ToLower() == "yes")
        {
            Console.WriteLine("Enter lesson start time (HH:mm):");
            TimeSpan startTime;
            while (!TimeSpan.TryParse(Console.ReadLine(), out startTime))
            {
                Console.WriteLine("Invalid time format. Please enter in HH:mm format.");
            }

            Console.WriteLine("Enter lesson name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter teacher's name:");
            string teacher = Console.ReadLine();

            Console.WriteLine("Enter lesson difficulty (1-10):");
            int difficulty;
            while (!int.TryParse(Console.ReadLine(), out difficulty) || difficulty < 1 || difficulty > 10)
            {
                Console.WriteLine("Invalid difficulty. Enter a number between 1 and 10.");
            }

            var lesson = new Lesson(name, teacher, difficulty, startTime);
            if (!ValidateSchedule(day, lesson))
            {
                Console.WriteLine("Time conflict detected! Choose a different time.");
                return;
            }

            AddOrUpdateLesson(day, lesson);
            Console.WriteLine("Schedule updated!");
        }
    }

    private bool ValidateSchedule(string day, Lesson newLesson)
    {
        if (!schedule.ContainsKey(day)) return true;

        foreach (var lesson in schedule[day])
        {
            if (!(newLesson.EndTime <= lesson.StartTime || newLesson.StartTime >= lesson.EndTime))
            {
                return false;
            }
        }
        return true;
    }

    public void AddOrUpdateLesson(string day, Lesson lesson)
    {
        day = new WeekDay().GetFormattedDay(day);
        if (!schedule.ContainsKey(day))
        {
            schedule[day] = new List<Lesson>();
        }
        schedule[day].Add(lesson);
        schedule[day].Sort();
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
                    writer.WriteLine(lesson.StartTime + " " + lesson.Name + " " + lesson.Teacher + " " + lesson.Difficulty);
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

