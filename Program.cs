using System.Text.Json;
class TaskTimer
{
    private DateTime? startTime;
    private double elapsedTime;
    private bool running;
    private List<LogEntry> logs;
    private readonly string logFile;

    public TaskTimer(string logFile = "timer_logs.json")
    {
        this.logFile = logFile;
        logs = LoadLogs();
        elapsedTime = 0;
        running = false;
    }

    private List<LogEntry> LoadLogs()
    {
        if (File.Exists(logFile))
        {
            try
            {
                var json= File.ReadAllText(logFile);
                return JsonSerializer.Deserialize<List<LogEntry>>(json) ?? new List<LogEntry>();
            catch
            {
                Console.WriteLine("Error reading logs. Starting with an empty log.");
            }
        }

        return new List<LogEntry>();
    }

    private void SaveLogs()
    {
        try
        {
             var json = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(logFile, json);
        }
        catch
        {
            Console.WriteLine("Error saving logs. Changes may not be persisted.");
        }
    }

    public void Start()
    {
        if (running)
        {
            Console.WriteLine("The timer is already running.");
        }
        else
        {
            startTime = DateTime.Now;
            running = true;
            Console.WriteLine("The timer has started.");
        }
    }

    public void Stop()
    {
        if (!running)
        {
            Console.WriteLine("The timer is not running.");
        }
        else
        {
            elapsedTime += (DateTime.Now - startTime.Value).TotalSeconds;
            startTime = null;
            running = false;
            Console.WriteLine($"The timer has stopped. Total elapsed time: {FormatTime(elapsedTime)}");
        }
    }

    public void Reset()
    {
        if (running)
        {
            Console.WriteLine("Stop the timer before resetting.");
        }
        else
        {
            elapsedTime = 0;
            Console.WriteLine("Timer reset.");
        }
    }

    public void LogSession(string taskName)
    {
        if (running)
        {
            Stop();
        }

        if (elapsedTime == 0)
        {
            Console.WriteLine("Cannot save an empty log. No time has been recorded.");
            return;
        }

        while (string.IsNullOrWhiteSpace(taskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name:");
            taskName = Console.ReadLine();
        }

        logs.Add(new LogEntry
        {
            TaskName = taskName,
            TimeSpent = FormatTime(elapsedTime)
        });
        SaveLogs();
        elapsedTime = 0;
        Console.WriteLine($"Task '{taskName}' has been logged.");
    }

    public void ShowLogs()
    {
        if (logs.Count == 0)
        {
            Console.WriteLine("No logs available.");
        }
        else
        {
            Console.WriteLine("\nTask Logs:");
            for (int i = 0; i < logs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Task: {logs[i].TaskName}, Time Spent: {logs[i].TimeSpent}");
            }
        }
    }

    public void DeleteLog()
    {
        if (logs.Count == 0)
        {
            Console.WriteLine("No logs available to delete.");
            return;
        }

        ShowLogs();
        Console.Write("Enter the number of the task you want to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= logs.Count)
        {
            logs.RemoveAt(index - 1);
            SaveLogs();
            Console.WriteLine("Log deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid task number.");
        }
    }

    public void ClearLogs()
    {
        Console.Write("Are you sure you want to clear all logs? (Y/N): ");
        string confirmation = Console.ReadLine()?.ToLower();

        if (confirmation == "y")
        {
            logs.Clear();
            SaveLogs();
            Console.WriteLine("All logs have been cleared.");
        }
        else if (confirmation == "n")
        {
            Console.WriteLine("Logs were not cleared.");
        }
        else
        {
            Console.WriteLine("Invalid input. Logs were not cleared.");
        }
    }

    private string FormatTime(double seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        return $"{(int)t.TotalHours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
    }

    public void Menu()
    {
        Console.WriteLine("\nAvailable commands: start, stop, reset, log, show, delete, clear, quit");

        while (true)
        {
            Console.Write("Enter command: ");
            string command = Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "start":
                    Start();
                    break;
                case "stop":
                    Stop();
                    break;
                case "reset":
                    Reset();
                    break;
                case "log":
                    Console.Write("Enter task name: ");
                    string taskName = Console.ReadLine();
                    LogSession(taskName);
                    break;
                case "show":
                    ShowLogs();
                    break;
                case "delete":
                    DeleteLog();
                    break;
                case "clear":
                    ClearLogs();
                    break;
                case "quit":
                    if (running)
                        Stop();
                    Console.WriteLine("Exiting application.");
                    return;
                default:
                    Console.WriteLine("Invalid command. Please try again.");
                    break;
            }
        }
    }

    private class LogEntry
    {
        public string TaskName { get; set; }
        public string TimeSpent { get; set; }
    }
}

class Program
{
    static void Main()
    {
        TaskTimer timer = new TaskTimer();
        timer.Menu();
    }
}
