using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TaskManager
{
    private readonly string? _filePath;
    private List<TodoItem>? _tasks;

    public TaskManager(string filePath)
    {
        _filePath = filePath;
        LoadTasks();
    }


    private void LoadTasks()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                _tasks = JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new();
            }
            else
            {
                // File exists but is empty — initialize to empty list
                _tasks = new();
            }
        }
        else
        {
            // Create an empty file and initialize in-memory list
            File.WriteAllText(_filePath, "[]");
            _tasks = new();
        }
    }

    public void SaveTasks()
    {
        string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void ListTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        for (int i = 0; i < _tasks.Count; i++)
        {
            var todo = _tasks[i];
            Console.WriteLine($"{i + 1}. {todo.Title} - Status: {todo.Status}\n");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void UpdateTaskStatus()
    {
        ListTasks();
        Console.WriteLine("Choose a task:");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _tasks.Count)
        {
            var selectedTask = _tasks[choice - 1];
            Console.WriteLine("Enter the new status (Active, Completed, Deleted): ");
            string? newStatus = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newStatus))
            {
                selectedTask.Status = newStatus;
                selectedTask.UpdatedAt = DateTime.UtcNow;
                SaveTasks();
                Console.WriteLine("Task updated successfully!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
    
    public void AddTask(string title, string notes)
    {
        var newTask = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Notes = notes,
            Status = "active",
            CreatedAt = DateTimeOffset.UtcNow
        };
        _tasks.Add(newTask);

        SaveTasks();
        Console.WriteLine("Task added successfully!");
    }

}