using CW_2course.Models;
using CW_2course.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CW_2course
{
    public partial class MainWindow : Window
    {
        public TaskListModel CurrentList { get; set; }

        public MainWindow(TaskListModel selectedList)
        {
            InitializeComponent();

            CurrentList = selectedList;

            if (CurrentList != null)
            {
                CurrentListTitle.Text = CurrentList.Title;

                TasksList.ItemsSource = CurrentList.Tasks;
            }
        }

        private bool _isDarkTheme = false;

        

        

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskModel selectedTask)
            {
                TaskWindow taskWindow = new TaskWindow(selectedTask);
                if (taskWindow.ShowDialog() == true)
                {
                   
                    int index = CurrentList.Tasks.IndexOf(selectedTask);
                    CurrentList.Tasks[index] = taskWindow.CurrentTask;

                    CurrentList.UpdateNearestDeadline();

                    TasksList.Items.Refresh();

                }
            }
            else
            {
                MessageBox.Show("Оберіть завдання для редагування!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var batchWindow = new BatchAddTaskWindow();
            if (batchWindow.ShowDialog() == true)
            {
                if (CurrentList != null && CurrentList.Tasks != null)
                {
                    foreach (var task in batchWindow.DraftTasks)
                    {
                        CurrentList.Tasks.Add(task);
                    }
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskModel selectedTask)
            {
                CurrentList.Tasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Оберіть завдання для видалення!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TaskCheckBox_Click(object sender, RoutedEventArgs e)
        {
           
            TasksList.Items.Refresh();
        }
    }
}