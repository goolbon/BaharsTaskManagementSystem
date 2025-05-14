using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BaharsTaskManagementSystem
{
    class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }

    class Program
    {
        static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            // Load tasks from JSON at start
            LoadTasks();
            while (true)
            {
                Console.WriteLine("1. Add Task  2. List Tasks  3. Delete Task  4. Exit");
                string choice = Console.ReadLine();
                if (choice == "1") { AddTask(); SaveTasks(); }
                else if (choice == "2") ListTasks();
                else if (choice == "3") { DeleteTask(); SaveTasks(); }
                else if (choice == "4") break;
                else Console.WriteLine("Invalid choice! Please enter 1, 2, 3, or 4.");
            }
        }

        static void AddTask()
        {
            try
            {
                string id;
                while (true)
                {
                    Console.WriteLine("Enter Task ID (e.g., 001) or 'cancel' to exit");
                    id = Console.ReadLine();
                    if (id.ToLower() == "cancel") return;
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        Console.WriteLine("Error: Task ID cannot be empty.");
                        continue;
                    }
                    if (tasks.Any(t => t.Id == id))
                    {
                        Console.WriteLine($"Error: Task ID '{id}' already exists.");
                        continue;
                    }
                    break;
                }

                string title;
                while (true)
                {
                    Console.WriteLine("Enter Task Title or 'cancel' to exit");
                    title = Console.ReadLine();
                    if (title.ToLower() == "cancel") return;
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("Error: Title cannot be empty.");
                        continue;
                    }
                    break;
                }

                Console.WriteLine("Enter Task Description (optional, press Enter to skip):");
                string description = Console.ReadLine();

                DateTime duedate;
                while (true)
                {
                    Console.WriteLine("Enter Task Due Date (YYYY-MM-DD) or 'cancel' to exit");
                    string dateInput = Console.ReadLine();
                    if (dateInput.ToLower() == "cancel") return;
                    if (string.IsNullOrWhiteSpace(dateInput))
                    {
                        Console.WriteLine("Error: Due Date cannot be empty.");
                        continue;
                    }
                    if (!DateTime.TryParse(dateInput, out duedate))
                    {
                        Console.WriteLine("Error: Invalid date format. Use YYYY-MM-DD (e.g., 2025-05-15).");
                        continue;
                    }
                    break;
                }

                string status;
                while (true)
                {
                    Console.WriteLine("Enter Status (Pending, Completed, e.g.) or 'cancel' to exit");
                    status = Console.ReadLine();
                    if (status.ToLower() == "cancel") return;
                    if (string.IsNullOrWhiteSpace(status))
                    {
                        Console.WriteLine("Error: Status cannot be empty.");
                        continue;
                    }
                    break;
                }

                Task task = new Task
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    DueDate = duedate,
                    Status = status
                };

                tasks.Add(task);
                Console.WriteLine("Task added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        static void ListTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }
            foreach (Task item in tasks)
            {
                Console.WriteLine($"ID: {item.Id}, Title: {item.Title}, Description: {item.Description}, Due: {item.DueDate.ToShortDateString()}, Status: {item.Status}");
            }
        }

        static void DeleteTask()
        {
            try
            {
                Console.WriteLine("Enter task ID to be deleted (e.g., 001):");
                string id = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Error: Task ID cannot be empty.");
                    return;
                }

                bool found = false;
                foreach (var item in tasks.ToList())
                {
                    if (item.Id == id)
                    {
                        tasks.Remove(item);
                        found = true;
                        break;
                    }
                }

                if (found) Console.WriteLine("Task deleted successfully!");
                else Console.WriteLine("Task not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        // Save tasks to JSON file
        static void SaveTasks()
        {
            try
            {
                File.WriteAllText("tasks.json", JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        // Load tasks from JSON file
        static void LoadTasks()
        {
            try
            {
                if (File.Exists("tasks.json"))
                {
                    tasks = JsonSerializer.Deserialize<List<Task>>(File.ReadAllText("tasks.json"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                tasks = new List<Task>();
            }
        }
    }
}