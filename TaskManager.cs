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

}