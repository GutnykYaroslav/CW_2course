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
        public ObservableCollection<TaskModel> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var loadedTasks = FileService.LoadTasks();
            Tasks = new ObservableCollection<TaskModel>(loadedTasks);
            TasksList.ItemsSource = Tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new TaskWindow();
            if (taskWindow.ShowDialog() == true)
            {
                // Перевіряємо, що вікно повернуло не null завдання
                if (taskWindow.CurrentTask != null)
                {
                    Tasks.Add(taskWindow.CurrentTask); // Додаємо на екран
                    FileService.SaveTasks(Tasks.ToList()); // Одразу записуємо у файл без помилок!
                }
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Увага: якщо підкреслює TasksList (CS0103) — заміни це слово на 
            // актуальне ім'я твого ListBox або DataGrid із файлу MainWindow.xaml!
            if (TasksList.SelectedItem is TaskModel selectedTask)
            {
                // Якщо підкреслює цей рядок (CS1729) — це означає, що у твоєму TaskWindow 
                // немає конструктора з параметром. Тоді міняй цей рядок на: new TaskWindow();
                TaskWindow taskWindow = new TaskWindow(selectedTask);
                if (taskWindow.ShowDialog() == true)
                {
                    int index = Tasks.IndexOf(selectedTask);
                    Tasks[index] = taskWindow.CurrentTask;

                    FileService.SaveTasks(Tasks.ToList()); // ВИПРАВЛЕНО CS1503
                }
            }
            else
            {
                MessageBox.Show("Оберіть завдання для редагування!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Тут так само: заміни TasksList на ім'я свого списку з XAML
            if (TasksList.SelectedItem is TaskModel selectedTask)
            {
                Tasks.Remove(selectedTask);
                FileService.SaveTasks(Tasks.ToList()); // ВИПРАВЛЕНО CS1503
            }
        }

        private void SaveTasks_Click(object sender, RoutedEventArgs e)
        {
            // Оскільки FileService потребує список (List), ми конвертуємо Tasks
            FileService.SaveTasks(Tasks.ToList());
            MessageBox.Show("Список завдань успішно збережено!", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportPdf_Click(object sender, RoutedEventArgs e)
        {
            // Створюємо екземпляр, бо метод не статичний, і конвертуємо в List
            PdfExportService pdfService = new PdfExportService();
            pdfService.ExportToPdf(Tasks.ToList());
        }
    }
}
