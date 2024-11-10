# CLI task timer - Programming languages project ( C#, Python, JavaScript )
# Introduction
The CLI Task Timer is a command-line tool developed using C#, JavaScript, and Python that allows you to start and stop timers for specific tasks and view a log of all recorded tasks with the time spent on each.
# The goals of the project
1. Provide a simple, efficient way to track time spent on tasks directly from the command line.
2. Demonstrate the use of CLI applications across multiple programming languages (C#, JavaScript, and Python).
3. Improve understanding of time management and basic CLI programming concepts.
# Key features
Here are the key features of the CLI task timer project:
- Start Timer: Begin tracking time for a specific task.
- Stop Timer: Stop tracking time once the task is completed.
- Task Log: View a list of all tasks with the corresponding time spent on each.
- Ephemeral Task Data: Task data is available during the current session, providing a simple, clean tracking experience without stored data.
# Technologies used
1. C#: For building the command-line interface and task timer functionality using the .NET framework.
2. JavaScript: Using Node.js to handle CLI operations and manage the task timer.
3. Python: For a lightweight, script-based approach to implementing the task timer in the terminal.
4. Github: For version control and project management across different programming languages.
# Instructions for Running and Using the Program
1. Clone the Repository:  
   Download or clone the project repository to your local machine.
2. Navigate to the Project Folder:  
   Open your terminal and go to the folder where the project is stored.
3. Run the Program:  
   Depending on the programming language you're using, run the appropriate command:
   - For C#:
     dotnet run
   - For JavaScript:
     node taskTimer.js
   - For Python:
     python task_timer.py
4. Using the Program:  
   - To start a task timer, use the command:
     tasktimer start "Task Name"
   - To stop a task timer, use the command:
     tasktimer stop "Task Name"
   - To view the task log, use the command:
     tasktimer log
     
Once you're finished, you can close the terminal to exit the program. Note that task data is only available during the current session and will be deleted when the program is closed
