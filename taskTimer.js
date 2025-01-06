const fs = require('fs');
const readlineSync = require('readline-sync');

class TaskTimer {
    constructor(logFile = "timer_logs.json") {
        this.startTime = null;
        this.elapsedTime = 0;
        this.running = false;
        this.logFile = logFile;
        this.loadLogs();
    }

    loadLogs() {
        try {
            if (fs.existsSync(this.logFile)) {
                this.logs = JSON.parse(fs.readFileSync(this.logFile));
            } else {
                this.logs = [];
            }
        } catch (err) {
            this.logs = [];
        }
    }

    saveLogs() {
        fs.writeFileSync(this.logFile, JSON.stringify(this.logs, null, 4));
    }

    start() {
        if (this.running) {
            console.log("The timer is already running.");
        } else {
            this.startTime = Date.now();
            this.running = true;
            console.log("The timer has started.");
        }
    }

    stop() {
        if (!this.running) {
            console.log("The timer is not running.");
        } else {
            this.elapsedTime += (Date.now() - this.startTime) / 1000;
            this.startTime = null;
            this.running = false;
            console.log(`The timer has stopped. Your total elapsed time: ${this.formatTime(this.elapsedTime)}`);
        }
    }

    reset() {
        if (this.running) {
            console.log("Stop the timer before resetting.");
        } else {
            this.elapsedTime = 0;
            console.log("Timer reset.");
        }
    }

    logSession(taskName) {

    
        if (this.elapsedTime === 0) {
            console.log("Cannot save an empty log. No time has been recorded.");
            return;
        }
    
        if (!taskName.trim()) {
            console.log("Task name cannot be empty.");
            return;
        }
    
        if (this.running) this.stop();
    
        const session = {
            "Your task": taskName,
            "Time spent on the task": this.formatTime(this.elapsedTime),
        };
    
        this.logs.push(session);
        this.saveLogs();
        this.elapsedTime = 0;
        console.log(`Task '${taskName}' has been logged successfully.`);
    }
    
    showLogs() {
        if (this.logs.length === 0) {
            console.log("No logs available.");
        } else {
            this.logs.forEach((log, index) => {
                console.log(`${index + 1}. Task: ${log['Your task']}, Time Spent: ${log['Time spent on the task']}`);
            });
        }
    }

    deleteLog() {
        if (this.logs.length === 0) {
            console.log("No logs available.");
            return;
        }
        console.log("Which task would you like to delete? Enter the task number:");
        this.showLogs();
        const index = parseInt(readlineSync.question("Enter task number to delete: ")) - 1;
        if (index >= 0 && index < this.logs.length) {
            this.logs.splice(index, 1);
            this.saveLogs();
            console.log("Log deleted.");
        } else {
            console.log("Invalid task number.");
        }
    }

    clearLogs() {
        if (this.logs.length === 0) {
            console.log("Nothing to delete.");
            return;
        }
        const confirmation = readlineSync.question("Are you sure you want to delete all logs? y/n ");
        if (confirmation.toLowerCase() === 'y'){
            this.logs = [];
            this.saveLogs();
            console.log("All logs cleared.");
        } else {
            console.log("Logs were not cleared.");
        }
    }

    menu() {
        console.log("\nSelect what you want to do: start, stop, reset, log, show, delete, clear, quit");
        const commands = {
            start: () => this.start(),
            stop: () => this.stop(),
            reset: () => this.reset(),
            log: () => {
                const taskName = readlineSync.question("Enter task name: ");
                this.logSession(taskName);
            },
            show: () => this.showLogs(),
            delete: () => this.deleteLog(),
            clear: () => this.clearLogs(),
            quit: () => {
                if (this.running) this.stop();
                console.log("Exiting the application.");
                process.exit(0);
            },
        };

        while (true) {
            const command = readlineSync.question("Enter command: ").toLowerCase();
            if (commands[command]) {
                commands[command]();
            } else {
                console.log("Invalid command. Try again.");
            }
        }
    }

    formatTime(seconds) {
        const hours = Math.floor(seconds / 3600);
        const minutes = Math.floor((seconds % 3600) / 60);
        const remainingSeconds = Math.floor(seconds % 60);
        
        return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:${String(remainingSeconds).padStart(2, '0')}`;
    }
}

const app = new TaskTimer();
app.menu();
