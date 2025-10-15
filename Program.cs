
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

List<TodoItem> tasks = new();
string filePath = "tasks.json";
var taskManager = new TaskManager(filePath);

do
{
    //Console.WriteLine("What would you like to do (enter the number):");
    Console.WriteLine("1. Add New Task");
    Console.WriteLine("2. Mark Task Complete");
    Console.WriteLine("3. View Tasks");
    Console.WriteLine("4. Delete Task");
    Console.WriteLine("5. Quit");

    Console.WriteLine("***********************************");
    Console.WriteLine("What would you like to do (enter the number):");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Enter the new task");
            string? title = Console.ReadLine();
            Console.WriteLine("Task Notes (optional):");
            string? notes = Console.ReadLine();

            var newTask = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = title,
                Notes = notes,
                Status = "active",
                CreatedAt = DateTimeOffset.UtcNow
            };
            tasks.Add(newTask);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(filePath, updatedJson);
            Console.Clear();
            Console.WriteLine($"\n✅ Added task: {newTask.Title}\n");
            break;
        case "2":
            
            Console.WriteLine("Complete task\n");
            break;
        case "3":
            Console.Clear();
            taskManager.ListTasks();
            break;
        case "4":
            Console.WriteLine("Delete task\n");
            break;
        case "5":

            return;
    }
} while (true);





