using CW_2course.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CW_2course.Services
{
    public static class FileService
    {
       
        private const string TasksFilePath = "tasks.json";

        public static void SaveLists(List<TaskListModel> lists)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(lists, options);
            File.WriteAllText(TasksFilePath, json);
        }

        public static List<TaskListModel> LoadLists()
        {
            if (!File.Exists(TasksFilePath)) return new List<TaskListModel>();

            string json = File.ReadAllText(TasksFilePath);
            var loadedLists = JsonSerializer.Deserialize<List<TaskListModel>>(json);
            return loadedLists ?? new List<TaskListModel>();
        }

        
        private const string UsersFilePath = "users.json";

        public static void SaveUsers(List<UserModel> users)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(UsersFilePath, json);
        }

        public static List<UserModel> LoadUsers()
        {
            if (!File.Exists(UsersFilePath)) return new List<UserModel>();

            string json = File.ReadAllText(UsersFilePath);
            var loadedUsers = JsonSerializer.Deserialize<List<UserModel>>(json);
            return loadedUsers ?? new List<UserModel>();
        }
    }
}
