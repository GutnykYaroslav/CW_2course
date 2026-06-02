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

        // 1. Конструктор за замовчуванням (для створення НОВОГО завдання)
        public TaskWindow()
            {
                InitializeComponent();
               // PriorityComboBox.ItemsSource = Enum.GetValues(typeof(TaskPriority));
               // ComplexityComboBox.ItemsSource = Enum.GetValues(typeof(TaskComplexity));

                DeadlinePicker.SelectedDate = DateTime.Now;
                ResultTask = new TaskModel();
            }

            // 2. Конструктор з параметром (для РЕДАГУВАННЯ існуючого завдання)
            public TaskWindow(TaskModel task) : this()
            {
                if (task != null)
                {
                    ResultTask = task;
                    TitleTextBox.Text = task.Title;
                    DeadlinePicker.SelectedDate = task.Deadline;
                    PriorityComboBox.SelectedItem = task.Priority;
                    ComplexityComboBox.SelectedItem = task.Complexity;
                    IsCompletedCheckBox.IsChecked = task.IsCompleted;
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

            // 2. ПАРСИМО СКЛАДНІСТЬ (відрізаємо пробіл і бали, залишаємо тільки перше слово)
            var complexity = CW_2course.Models.TaskComplexity.Середньо; // Нормально замість Середньо!
            if (!string.IsNullOrEmpty(ComplexityComboBox.Text))
            {
                string cleanComplexity = ComplexityComboBox.Text.Split(' ')[0];
                System.Enum.TryParse<CW_2course.Models.TaskComplexity>(cleanComplexity, out complexity);
            }


            // 3. Створюємо об'єкт завдання
            CurrentTask = new TaskModel
            {
                Title = TitleTextBox.Text,
                Deadline = DeadlinePicker.SelectedDate ?? DateTime.Now,
                Priority = priority,
                Complexity = complexity, // Сюди піде чисте значення (Легко, Нормально або Складно)
                IsCompleted = false
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
