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
- Pause Timer: Temporarily stop the timer for a task. Users can then resume the timer later, allowing breaks without affecting the total tracked time.
- Continue Timer: Resume tracking time after a pause. This adds flexibility for tasks that might be interrupted.
- Task Log: View a list of all tasks with the corresponding time spent on each.
- Persistent Task Data: Task data is stored in a JSON file, allowing users to track their tasks across sessions. Each session's data is appended to or loaded from this JSON file, providing a way to keep track of task history.
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
   - Starting a Task: Begin tracking time for a task by using the start command, followed by the task’s name. This will start the timer for that specific task.
   - Pausing a Task: If you need to take a break, you can pause the timer for an ongoing task. This will temporarily halt time tracking but will keep the task active in the system.
   - Resuming a Task: When ready to continue working on a paused task, use the resume command. This will restart time tracking from where it left off, without resetting the accumulated time.
   - Stopping a Task: Once you’ve completed a task, you can stop the timer. This will finalize the tracked time for the task and store it, including any pauses.
   - Viewing the Task Log: To see a summary of all tasks and the time spent on each, use the log command. This will display details for each task, including total time and any pauses, and the information is saved in a JSON format for future reference
     
Once you're finished, simply close the terminal to exit the program. All data will be saved in a JSON format, allowing you to access your task records at any time.
