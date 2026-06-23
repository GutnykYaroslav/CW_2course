using CW_2course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CW_2course
{
    public partial class TaskWindow : Window
    {
        public TaskModel ResultTask { get; private set; }
        public TaskModel CurrentTask { get; set; }

        public TaskWindow()
        {
            InitializeComponent();
            DeadlinePicker.SelectedDate = DateTime.Now;
            ResultTask = new TaskModel();
        }

        public TaskWindow(TaskModel task) : this()
        {
            if (task != null)
            {
                ResultTask = task;
                TitleTextBox.Text = task.Title;
                DeadlinePicker.SelectedDate = task.Deadline;
                PriorityComboBox.SelectedItem = task.Priority;
                IsCompletedCheckBox.IsChecked = task.IsCompleted;

               
                this.Loaded += (s, e) =>
                {
                    DifficultyComboBox.SelectedIndex = -1;
                    DifficultyComboBox.SelectedItem = null;
                    DifficultyComboBox.Text = string.Empty;
                };

                if (!string.IsNullOrEmpty(task.Difficulty))
                {
                    foreach (ComboBoxItem item in DifficultyComboBox.Items)
                    {
                        if (item.Content != null && item.Content.ToString() == task.Difficulty)
                        {
                            DifficultyComboBox.SelectedItem = item;
                            break;
                        }
                    }

                   
                    if (DifficultyComboBox.SelectedItem == null)
                    {
                        DifficultyComboBox.Text = task.Difficulty;
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Будь ласка, введіть назву завдання!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            System.Enum.TryParse<CW_2course.Models.TaskPriority>(PriorityComboBox.Text, out var priority);

           
            string selectedDifficulty = "Нормально (5 балів)"; 
            if (DifficultyComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content != null)
            {
                selectedDifficulty = selectedItem.Content.ToString();
            }
            else if (!string.IsNullOrEmpty(DifficultyComboBox.Text))
            {
                selectedDifficulty = DifficultyComboBox.Text;
            }

            CurrentTask = new TaskModel
            {
                Title = TitleTextBox.Text,
                Deadline = DeadlinePicker.SelectedDate ?? DateTime.Now,
                Priority = priority,
                Difficulty = selectedDifficulty, 
                IsCompleted = IsCompletedCheckBox.IsChecked ?? false
            };

            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}