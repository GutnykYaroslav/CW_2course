using CW_2course.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CW_2course.Services
{
    public static class FileService
    {
        private static readonly string FilePath = "tasks.txt"; // або твій шлях до файлу

        // БЕЗПЕЧНЕ ЗБЕРЕЖЕННЯ
        public static void SaveTasks(List<TaskModel> tasks)
        {
            if (tasks == null) return;

            List<string> lines = new List<string>();
            foreach (var t in tasks)
            {
                // ГОЛОВНИЙ ЗАХИСТ: якщо об'єкт завдання чомусь null, просто пропускаємо його і йдемо далі!
                if (t == null) continue;

                lines.Add($"{t.Title}|{t.Deadline:yyyy-MM-dd}|{t.Priority}|{t.Complexity}|{t.IsCompleted}");
            }

            File.WriteAllLines(FilePath, lines);
        }

        // БЕЗПЕЧНЕ ЗАВАНТАЖЕННЯ
        public static List<TaskModel> LoadTasks()
        {
            List<TaskModel> list = new List<TaskModel>();
            if (!File.Exists(FilePath)) return list;

            string[] lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                // Пропускаємо повністю порожні рядки у файлі
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split('|');
                if (parts.Length < 5) continue; // Якщо рядок пошкоджений — ігноруємо його

                try
                {
                    var task = new TaskModel
                    {
                        Title = parts[0],
                        Deadline = DateTime.Parse(parts[1]),
                        Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), parts[2]),
                        Complexity = (TaskComplexity)Enum.Parse(typeof(TaskComplexity), parts[3]),
                        IsCompleted = bool.Parse(parts[4])
                    };

                    if (task != null)
                    {
                        list.Add(task);
                    }
                }
                catch
                {
                    // Якщо якийсь один рядок записаний криво, додаток не впаде, а просто піде далі
                    continue;
                }
            }
            return list;
        }
    }
}
