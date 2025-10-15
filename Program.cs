
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
    Console.Clear();
    //Console.WriteLine("What would you like to do (enter the number):");
    Console.WriteLine("1. Add New Task");
    Console.WriteLine("2. Mark Task Complete");
    Console.WriteLine("3. View Tasks");
    Console.WriteLine("4. Quit");

    Console.WriteLine("***********************************");
    Console.WriteLine("What would you like to do (enter the number):");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Enter the new task");
            string? title = Console.ReadLine() ?? "";
            Console.WriteLine("Task Notes (optional):");
            string? notes = Console.ReadLine() ?? "";

            taskManager.AddTask(title, notes);
            break;
        case "2":
            Console.Clear();
            taskManager.UpdateTaskStatus();
            break;
        case "3":
            Console.Clear();
            taskManager.ListTasks();
            break;
        case "4":
            return;
    }
} while (true);





