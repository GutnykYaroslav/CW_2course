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
using System.Windows.Shapes;

namespace CW_2course
{
   
    public partial class MainMenuWindow : Window
    {
        public ObservableCollection<TaskListModel> AllLists { get; set; }
        public MainMenuWindow()
        {
            InitializeComponent();

           
            var loadedLists = FileService.LoadLists();
            if (loadedLists != null && loadedLists.Count > 0)
            {
                AllLists = new ObservableCollection<TaskListModel>(loadedLists);
            }
            else
            {
                AllLists = new ObservableCollection<TaskListModel>();
            }

            ListsListBox.ItemsSource = AllLists;
        }

        private void CreateList_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewListTitleInput.Text))
            {
               
                AllLists.Add(new TaskListModel
                {
                    Title = NewListTitleInput.Text,
                    Tasks = new ObservableCollection<TaskModel>()
                });
                NewListTitleInput.Clear();
                FileService.SaveLists(AllLists.ToList()); 
            }
        }

        private void DeleteList_Click(object sender, RoutedEventArgs e)
        {
            if (ListsListBox.SelectedItem is TaskListModel selectedList)
            {
                AllLists.Remove(selectedList);
                FileService.SaveLists(AllLists.ToList());
            }
        }

        private void OpenList_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedList();
        }

        private void ListsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSelectedList();
        }

       
        private void OpenSelectedList()
        {
            if (ListsListBox.SelectedItem is TaskListModel selectedList)
            {
            
                MainWindow taskWindow = new MainWindow(selectedList);

                this.Hide(); 
                taskWindow.ShowDialog(); 
                this.Show(); 

               
                FileService.SaveLists(AllLists.ToList());
            }
        }
    }
}
