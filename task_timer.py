import time
import json


class Timer:
    def __init__(self, log_file="timer_logs.json"):
        self.start_time = None
        self.elapsed_time = 0
        self.running = False
        self.log_file = log_file
        self.load_logs()

    def load_logs(self):
        try:
            with open(self.log_file, "r") as file:
                self.logs = json.load(file)
        except (FileNotFoundError, json.JSONDecodeError):
            self.logs = []

    def save_logs(self):
        with open(self.log_file, "w") as file:
            json.dump(self.logs, file, indent=4)

    def start(self):
        if self.running:
            print("The timer is already running.")
        else:
            self.start_time = time.time()
            self.running = True
            print("The timer has started.")

    def stop(self):
        if not self.running:
            print("The timer is not running.")
        else:
            self.elapsed_time += time.time() - self.start_time
            self.start_time = None
            self.running = False
            print(f"The timer has stopped. Your total elapsed time: {self.format_time(self.elapsed_time)}")

    def reset(self):
        if self.running:
            print("Stop the timer before resetting.")
        else:
            self.elapsed_time = 0
            print("Timer reset.")

    def log_session(self, task_name):
        if not self.running:
            print("You cannot log a task while the timer is not running.")
            return

        while not task_name.strip():
            print("Task name cannot be empty. Please try again.")
            task_name = input("Enter task name: ").strip()

        if self.running:
            self.stop()
        session = {
            "Your task": task_name,
            "Time spent on the task": self.format_time(self.elapsed_time),
        }
        self.logs.append(session)
        self.save_logs()
        self.elapsed_time = 0
        print(f"Task '{task_name}' has been logged successfully.")

    def format_time(self, seconds):
        mins, secs = divmod(int(seconds), 60)
        hrs, mins = divmod(mins, 60)
        return f"{hrs:02}:{mins:02}:{secs:02}"

    def show_logs(self):
        if not self.logs:
            print("No logs available.")
        else:
            for i, log in enumerate(self.logs, 1):
                print(f"{i}. Task: {log['Your task']}, Time Spent: {log['Time spent on the task']}")

    def delete_logs(self):
        if not self.logs:
            print("No logs available.")
        else:
            print("Which task would you like to delete?")
            try:
                index = int(input("Enter task number to delete: ").strip())
                self.logs.pop(index - 1)
                self.save_logs()
                print("Log deleted successfully.")
            except (ValueError, IndexError):
                print("Invalid input. Please enter a valid task number.")

    def clear_logs(self):
        if not self.logs:
            print("Nothing to delete.")
        else:
            print("Are you sure you want to delete all logs? (Y/N)")
            choice = input().strip().lower()
            if choice == "y":
                self.logs = []
                self.save_logs()
                print("All logs have been cleared.")
            elif choice == "n":
                print("Logs not cleared.")
            else:
                print("Invalid input.")

    def menu(self):
        print("\nSelect an action: start, stop, reset, log, show, delete, clear, quit")
        while True:
            command = input("Enter command: ").strip().lower()
            if command == "start":
                self.start()
            elif command == "stop":
                self.stop()
            elif command == "reset":
                self.reset()
            elif command == "log":
                task_name = input("Enter task name: ").strip()
                self.log_session(task_name)
            elif command == "show":
                self.show_logs()
            elif command == "delete":
                self.delete_logs()
            elif command == "clear":
                self.clear_logs()
            elif command == "quit":
                if self.elapsed_time > 0 or self.running:
                    if self.running:
                        self.stop()
                    self.log_session("N/A")
                print("Exiting the application.")
                break
            else:
                print("Invalid command. Please try again.")


if __name__ == "__main__":
    app = Timer()
    app.menu()
