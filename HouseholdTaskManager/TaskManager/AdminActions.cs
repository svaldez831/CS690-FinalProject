namespace TaskManager;
using Spectre.Console;
public static class AdminActions {
    public static int ViewAllTasks() {
        Console.Clear();
        var tasks = FileHelper.ReadTasksFromFile("records.txt");
        Console.WriteLine("== All Tasks ==");
        foreach(var task in tasks) {
            Console.WriteLine("\n" + task.DisplayAllString());
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        return tasks.Count;
    }

    public static int ViewAllBills() {
        Console.Clear();
        var bills = FileHelper.ReadBillsFromFile("records.txt");
        Console.WriteLine("== All Bills ==");
        foreach(var bill in bills) {
            Console.WriteLine("\n" + bill.DisplayAllString());
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        return bills.Count;
    }
    public static int ViewAllUsers() {
        Console.Clear(); 
        var users = FileHelper.GenerateUsers("records.txt");
        Console.WriteLine("=== All Users ===");
        foreach (var user in users) {
            Console.WriteLine(user);
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        return users.Count;
    }

    public static int ViewTasksCompleted() {
        var tasks = FileHelper.ReadTasksFromFile("records.txt");
        var completed = RecordFilter.ExtractTasksCompleted(tasks);
         Console.WriteLine("=== All Completed Tasks ===");
        foreach(Task row in completed) {
            Console.WriteLine(row.DisplayAllString());
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return completed.Count;
    }

    public static int ViewBillsPaid() {
        var bills = FileHelper.ReadBillsFromFile("records.txt");
        var completed = RecordFilter.ExtractBillsCompleted(bills);
         Console.WriteLine("=== All Paid Bills ===");
        foreach(Bill bill in completed) {
            Console.WriteLine(bill.DisplayAllString());
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        return completed.Count;
    }

    public static void addNewPriorities(string textFile) {
        var TypeSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What new Priority is this?")
            .PageSize(10)
            .AddChoices("Task", "Bill")
        );
        
        if(TypeSelection == "Task") {
            
            Task result = new Task(" ", " ", " ", " ");
            result.Id = DateTime.Now.Ticks.ToString();
            result.Type = TypeSelection;
            Console.Write("Who shall we assign this to? Type Name: \n");

            result.Assignee = Console.ReadLine();
            Console.Write("What is the name of this " + TypeSelection +"\n");
            string priorityName = Console.ReadLine();
            result.Name = priorityName;
            string dateCreated = DateTime.Now.ToString("dd-MM-yyyy");
            string dateModified = DateTime.Now.ToString("dd-MM-yyyy");
            result.DateCreated = dateCreated;
            result.DateModified = dateModified;
            Console.Write("When is this due? Enter in dd-MM-YYYY format.\nExample: " + DateTime.Now.ToString("dd-MM-yyyy") +" dd = day, MM = Month, yyyy = Year\n");
            string dueDate = Console.ReadLine();
            result.DueDate = dueDate;
            result.Completed = false;
            Console.Write("Add additonal Instructions " +TypeSelection+ "\n");
            string addInstructions = Console.ReadLine();
            result.Instructions = addInstructions;
            string placeHolderNote = "Notes ";
            result.Notes = placeHolderNote;

            Console.WriteLine("\nConfirm the entry information");
            Console.WriteLine(result.DisplayAllString());
            
      

            var taskSave = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please Confirm new Entry Lookds Good!")
                .PageSize(10)
                .AddChoices("Yes", "No")
            );

            if (taskSave == "Yes") {
                var priorityToSave = new List<object>{result};
                FileHelper.SaveToFile2(priorityToSave, textFile);                                
                Console.WriteLine($"New Entry '{result.Name}' has been Saved Successfully");
            }
            else {
                Console.WriteLine("Entry was not save! Exiting...");
            }


        }
        else {
            Bill result = new Bill(" ", " ", " ", " ");
            result.Id = DateTime.Now.Ticks.ToString();
            result.Type= TypeSelection;
            Console.Write("Who shall we assign this to? Type Name: \n");

            result.Assignee = Console.ReadLine();
            Console.Write("What is the name of this " + TypeSelection +"\n");
            string priorityName = Console.ReadLine();
            result.Name = priorityName;
            string dateCreated = DateTime.Now.ToString("dd-MM-yyyy");
            string dateModified = DateTime.Now.ToString("dd-MM-yyyy");
            result.DateCreated = dateCreated;
            result.DateModified = dateModified;
            Console.Write("When is this due? Enter in dd-MM-YYYY format.\nExample: " + DateTime.Now.ToString("dd-MM-yyyy") +" dd = day, MM = Month, yyyy = Year\n");
            string dueDate = Console.ReadLine();
            result.DueDate = dueDate;
            result.Paid = false;
            Console.Write("Add additonal Instructions " +TypeSelection+ "\n");
            string addInstructions = Console.ReadLine();
            result.Instructions = addInstructions;
            string placeHolderNote = "Notes ";
            result.Notes = placeHolderNote;

            Console.WriteLine("\nConfirm the entry information");
            Console.WriteLine(result.DisplayAllString());

            var saveBill = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please Confirm new Entry Lookds Good!")
                .PageSize(10)
                .AddChoices("Yes", "No")
            );

            if (saveBill == "Yes") {
                var priorityToSave = new List<object>{result};
                FileHelper.SaveToFile2(priorityToSave, textFile);                                
                Console.WriteLine($"New Entry '{result.Name}' has been Saved Successfully");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else {
                Console.WriteLine("Entry was not save! Exiting...");
            }


        }

        
        
    }
    
public static void GenerateReport() {
    int allTasks = ViewAllTasks();
    int allBills = ViewAllBills();
    int completedTasks = ViewTasksCompleted();
    int paidBills = ViewBillsPaid();
    Console.WriteLine($"There are in total of {allTasks} Tasks and {allBills} Bills in Saved");
    Console.WriteLine($"{completedTasks} Tasks are Completed and {allTasks - completedTasks} are still Open");
     Console.WriteLine($"{paidBills} Bills are Paid and {allBills  - paidBills} are still Open");

}





}