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

    private void SaveTasks()
    {

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
    }

    private void UpdateTaskStatus()
    {

    }
    
    private void AddTask(string title, string notes)
    {
        
    }

}