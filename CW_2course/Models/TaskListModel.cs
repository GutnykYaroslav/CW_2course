using CW_2course.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CW_2course
{
    public class TaskListModel
    {
       
        public Guid Id { get; set; } = Guid.NewGuid();

     
        public string Title { get; set; }

        
        public ObservableCollection<TaskModel> Tasks { get; set; } = new ObservableCollection<TaskModel>();

        
        public DateTime? NearestDeadline { get; set; }

     
        public void UpdateNearestDeadline()
        {
            
            var activeTasks = Tasks.Where(t => !t.IsCompleted).ToList();

            if (activeTasks.Any())
            {
               
                NearestDeadline = activeTasks.Min(t => t.Deadline);
            }
            else
            {
               
                NearestDeadline = null;
            }
        }
    }
}