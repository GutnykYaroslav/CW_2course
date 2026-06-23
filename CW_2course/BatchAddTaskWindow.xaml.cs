using CW_2course.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CW_2course
{

    public partial class BatchAddTaskWindow : Window
    {
        // Тимчасовий список для зберігання завдань
        public List<TaskModel> DraftTasks { get; private set; } = new List<TaskModel>();

        public BatchAddTaskWindow()
        {
            InitializeComponent();
        }

        private void AddToDraftBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DraftTasks.Count >= 5)
            {
                MessageBox.Show("Ви досягли ліміту! Можна додати до 5 завдань за один раз.", "Ліміт", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(TitleInput.Text))
            {
                MessageBox.Show("Будь ласка, введіть хоча б назву завдання.", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTask = new TaskModel
            {
                Title = TitleInput.Text,
                Description = DescInput.Text,
                Deadline = DateTime.Now.AddDays(1) // Дедлайн за замовчуванням на завтра
            };

            DraftTasks.Add(newTask);
            DraftListBox.Items.Add($"• {newTask.Title}");

            DraftCountLabel.Text = $"В черзі: {DraftTasks.Count} / 5";

            TitleInput.Clear();
            DescInput.Clear();

            if (DraftTasks.Count == 5)
            {
                AddToDraftBtn.IsEnabled = false;
                DraftCountLabel.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void SaveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DraftTasks.Count == 0)
            {
                MessageBox.Show("Черга порожня. Додайте хоча б одне завдання!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
