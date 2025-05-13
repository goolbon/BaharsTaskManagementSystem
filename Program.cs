using System;
using System.Collections.Generic;
using System.Linq;

namespace BaharsTaskManagementSystem
{
    class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        //test this 
        //this too
    }
    class Program
    {

        static List<Task> tasks = new List<Task>();
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Add Task  2. List Tasks  3. Delete Task  4. Exit");
                string choice = Console.ReadLine();
                if (choice == "1") AddTask();
                else if (choice == "2") ListTasks();
                else if (choice == "3") DeleteTask();
                else if (choice == "4") break;

            }
        }
        static void AddTask()
        {
            try // try: Starts block to catch errors
            {

                Console.WriteLine("Enter Task ID");
                string id = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(id))
                    {
                        throw new ArgumentException("Task ID cannot be empty!");
                    }
                if (tasks.Any(t => t.Id == id))
                    {
                        throw new ArgumentException("Task ID already exists!");
                    }
                        Console.WriteLine("Enter Task Title");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                    {
                        throw new ArgumentException("Title can't be empty!");
                    }
                Console.WriteLine("Enter Task Description:");
                string description = Console.ReadLine();
                Console.WriteLine("Enter Task Due Date (YYYY-MM-DD):");
                string duedate = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(duedate))
                    {
                        throw new ArgumentException("Due Date cannot be empty.");
                    }
                if (!DateTime.TryParse(duedate, out DateTime outputdate))
                    {
                        throw new FormatException("Invalid date format. Use YYYY-MM-DD (e.g., 2025-05-15).");
                    }

                Console.WriteLine("Enter Status (Pending , Completed , e.g.)");
                string status = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(status))
                    {
                        throw new ArgumentException("Status cannot be empty.");
                    }

                Task task = new Task
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    DueDate = duedate,
                    Status = status,
                };

                tasks.Add(task);
                Console.WriteLine("Task added successfully");
            }
            
        catch (FormatException ex)
        {
            // Console.WriteLine(...): Shows error
            Console.WriteLine($"Invalid input: {ex.Message}");
        }
        // catch (ArgumentException ex): Catches validation errors
        catch (ArgumentException ex)
        {
            // Console.WriteLine(...): Shows error
            Console.WriteLine($"Error: {ex.Message}");
        }
        // catch (Exception ex): Catches unexpected errors
        catch (Exception ex)
        {
            // Console.WriteLine(...): Shows error
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        }
        static void ListTasks()
        {
            foreach (Task item in tasks)
            {
                Console.WriteLine($"ID: {item.Id}, " +
                                  $"Title: {item.Title}, " +
                                  $"Description: {item.Description}, " +
                                  $"Due Date: {item.DueDate.ToString()}, " +
                                  $"Status: {item.Status}");

            }
        }

        static void DeleteTask()
        {
            Console.WriteLine("Enter task ID to be deleted:");
            string id = Console.ReadLine();
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
            if (found)
            {
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("Task not found!");

            }
        }
    }
}
