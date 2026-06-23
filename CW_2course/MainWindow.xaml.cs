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

        public ObservableCollection<TaskModel> GlobalTasks { get; set; } = new ObservableCollection<TaskModel>();


        public TaskListModel SelectedList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TasksList.ItemsSource = GlobalTasks;

            var loadedLists = FileService.LoadLists();
            if (loadedLists != null && loadedLists.Count > 0)
            {
              
                GlobalTasks = loadedLists[0].Tasks;
            }
            else
            {
               
                GlobalTasks = new ObservableCollection<TaskModel>();
            }

           
            TasksList.ItemsSource = GlobalTasks;



        }

        private bool _isDarkTheme = false;

        

       

        private void ExportToPdfButton_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedList == null)
            {
                MessageBox.Show("Оберіть список для експорту!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "PDF файл (*.pdf)|*.pdf";
            saveFileDialog.FileName = $"Мої_Завдання_{SelectedList.Title}.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    PdfExportService exporter = new PdfExportService();

                    exporter.Export(SelectedList.Tasks, saveFileDialog.FileName);

                    MessageBox.Show("PDF успішно збережено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }
        private void SaveTasks_Click(object sender, RoutedEventArgs e)
        {

            var listsToSave = new List<TaskListModel>
{
    new TaskListModel { Title = "Головний список", Tasks = GlobalTasks }
};
            FileService.SaveLists(listsToSave);
            MessageBox.Show("Всі списки успішно збережено!", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskModel selectedTask && SelectedList != null)
            {
                TaskWindow taskWindow = new TaskWindow(selectedTask);
                if (taskWindow.ShowDialog() == true)
                {
                    int index = SelectedList.Tasks.IndexOf(selectedTask);
                    SelectedList.Tasks[index] = taskWindow.CurrentTask;


                    SelectedList.UpdateNearestDeadline();

                    var listsToSave = new List<TaskListModel>
                    {
                    new TaskListModel { Title = "Головний список", Tasks = GlobalTasks }
                    };
                    FileService.SaveLists(listsToSave);
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
               
                foreach (var task in batchWindow.DraftTasks)
                {
                    GlobalTasks.Add(task);
                }


                var listsToSave = new List<TaskListModel>
                {
                new TaskListModel { Title = "Головний список", Tasks = GlobalTasks }
                };
                FileService.SaveLists(listsToSave);


                TasksList.Items.Refresh();
            }
        }


        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskModel selectedTask)
            {
                GlobalTasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Оберіть завдання для видалення!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
    }
}