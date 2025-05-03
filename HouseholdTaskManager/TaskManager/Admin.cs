namespace TaskManager;
using Spectre.Console;
public class Admin {
    public string Username {get;  private set; }
    public Admin(string username) {
        Username = username;
    }

    public void ShowAdminMenu() {
        bool running = true;
        while (running) {
            Console.Clear();
            Console.WriteLine($"== Welcome Back Admin: ({Username}) ==" );
            var selectionOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please Select an option to Continue:")
                .PageSize(10)
                .AddChoices("1. View All tasks", "2. View All Bills", "3. View All Users", "4. View Completed", "5. View Paid ", "6. Create New Tasks or Bills", "7. Generate Report", "8. Exit Admin")
            );

            switch (Char.ToString(selectionOption[0])) {
                case "1":
                    AdminActions.ViewAllTasks();
                    break;
                case "2":
                    AdminActions.ViewAllBills();
                    break;
                case "3":
                    AdminActions.ViewAllUsers();
                    break;
                case "4":
                    AdminActions.ViewTasksCompleted();
                    break;
                case "5":
                    AdminActions.ViewBillsPaid();
                    break;
                case "6":
                    AdminActions.addNewPriorities("records.txt");                    
                    break;
                case "7":
                    AdminActions.GenerateReport();
                    break;
                case "8":
                    running = false;
                    break;

            }
        }
    }


}