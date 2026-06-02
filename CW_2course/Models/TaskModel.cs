using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_2course.Models
{
    public enum TaskPriority { Низький, Середній, Високий, Критичний }
    public enum TaskComplexity { Легко, Середньо, Складно }

    public class TaskModel
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Deadline { get; set; } = DateTime.Now;
        public TaskPriority Priority { get; set; } = TaskPriority.Середній;
        public TaskComplexity Complexity { get; set; } = TaskComplexity.Середньо;
        public bool IsCompleted { get; set; }

        public bool IsOverdue => !IsCompleted && Deadline < DateTime.Now;
    }
}
