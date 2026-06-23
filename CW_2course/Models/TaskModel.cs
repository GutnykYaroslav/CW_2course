using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_2course.Models
{
    public enum TaskPriority
    {
        Низький,
        Середній,
        Високий,
        Критичний
    }

    public enum TaskComplexity
    {
        Легко,
        Середньо,
        Складно
    }

    public class TaskModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public TaskPriority Priority { get; set; }
        public TaskComplexity Complexity { get; set; }
        public bool IsCompleted { get; set; } // Залишили тільки ОДИН раз

        public bool IsOverdue => !IsCompleted && Deadline < DateTime.Now;

        public string ComplexityDisplay
        {
            get
            {
                switch (Complexity)
                {
                    case TaskComplexity.Легко: return "Легко (3 балів)";
                    case TaskComplexity.Середньо: return "Середньо (5 балів)";
                    case TaskComplexity.Складно: return "Складно (10 балів)";
                    default: return Complexity.ToString();
                }
            }
        }
    }
}
