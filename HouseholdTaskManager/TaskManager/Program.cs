namespace TaskManager;
using System;//writeline
using System.Collections.Generic;//collections
using System.IO;//readlines
using Spectre.Console;
using System.Linq;

class Program
{
    static void Main(string[] args) { 
        bool isRunning = true;
        Console.Clear();
        while(isRunning) {
            
            string textFile = "records.txt";
            List<string> users = FileHelper.GenerateUsers(textFile);
            var userSelect = users;
            var tasks = FileHelper.ReadTasksFromFile(textFile);
            var bill = FileHelper.ReadBillsFromFile(textFile);
            userSelect.Add("Admin");
            userSelect.Add("Exit");
            var selectionOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please Select a User to Continue:")
                .PageSize(10)
                .AddChoices(userSelect)
            );
            if(selectionOption == "Admin") {
                
                Console.WriteLine("Enter Admin username: ");
                string adminName = Console.ReadLine();
                Admin admin = new Admin(adminName);
                admin.ShowAdminMenu();

            }
            else if( selectionOption == "Exit") {
                isRunning = false;
            }
            else {
                Console.Clear();
                Console.WriteLine("User ModeActivated!");
                AnsiConsole.MarkupLine($"[bold blue] Welcome Back {selectionOption}[/]" );
                var userSelectedBills= RecordFilter.ExtractBillByUser(bill,selectionOption);
                var userSelectedTasks = RecordFilter.ExtractTaskByUser(tasks,selectionOption);
                Console.Write($"\nFound ({userSelectedTasks.Count}) Task(s) for {selectionOption} ,");
                Console.WriteLine($"Found ({userSelectedBills.Count}) bills(s) for {selectionOption}");    

                var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select Task or Bill to Interact")
                    .PageSize(10)
                    .AddChoices("Task", "Bill", "Exit")
                );  
                if(option == "Task") {
                    var taskSelected = AnsiConsole.Prompt(
                        new SelectionPrompt<Task>()
                        .Title("Select Task to See More Details: ")
                        .PageSize(10)
                        .UseConverter(t=> $"{t.Name}")
                        .AddChoices(userSelectedTasks)
                    ); 
                    Console.WriteLine(taskSelected.ToDisplayString());
                    var taskPayed = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Is this Task Completed?")
                        .PageSize(10)
                        .AddChoices("Yes", "No")
                    );

                    if(taskPayed == "Yes") {
                        taskSelected.Completed = true;
                        taskSelected.DateModified = DateTime.Now.ToString("dd-MM-yyyy");
                        var addNote = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Would You like to Add a note?")
                            .PageSize(10)
                            .AddChoices("Yes", "No")
                        );
                        if(addNote == "Yes") {
                            Console.WriteLine("Add your Note: ");
                            taskSelected.Notes = Console.ReadLine();
                            Console.WriteLine($"Task '{taskSelected.Name}' has been Marked as Complete as of {taskSelected.DateModified}");
                            
                            var saveChanges = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("Save Changes?")
                                .PageSize(10)
                                .AddChoices("Yes", "No")
                            );

                            if(saveChanges == "Yes") {
                                var taskToSave = new List<object>{taskSelected};
                                FileHelper.SaveToFile2(taskToSave);                                
                                Console.WriteLine($"Changes to Task: '{taskSelected.Name}' has been Saved Successfully");
                            } 
                            else{
                                Console.WriteLine($"No Changes to Task: '{taskSelected.Name}' were made");
                            }
                        
                        }
                        else {
                            Console.WriteLine($"Task '{taskSelected.Name}' has been Marked as Complete as of {taskSelected.DateModified}");
                            var taskToSave = new List<object>{taskSelected};
                            FileHelper.SaveToFile2(taskToSave);                                
                            Console.WriteLine($"Changes to Task: '{taskSelected.Name}' has been Saved Successfully");
                        }

                    }
                    else {
                        Console.WriteLine($"No Changes Made to Task: {taskSelected.Name}");
                    }
                }
                else {
                        var billSelected = AnsiConsole.Prompt(
                        new SelectionPrompt<Bill>()
                        .Title("Select Bill to See More Details: ")
                        .PageSize(10)
                        .UseConverter(t=> $"{t.Name}")
                        .AddChoices(userSelectedBills)
                    ); 
                    Console.WriteLine(billSelected.ToDisplayString());
                    var billPayed = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Is this Bill Paid Off?")
                        .PageSize(10)
                        .AddChoices("Yes", "No")
                    );

                    if(billPayed == "Yes") {
                        billSelected.Paid = true;
                        billSelected.DateModified = DateTime.Now.ToString("dd-MM-yyyy");
                        var addNote = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Would You like to Add a note?")
                            .PageSize(10)
                            .AddChoices("Yes", "No")
                        );
                        if(addNote == "Yes") {
                            Console.WriteLine("Add your Note: ");
                            billSelected.Notes = Console.ReadLine();
                            Console.WriteLine($"Bill '{billSelected.Name}' has been Marked as Payed as of {billSelected.DateModified}");

                            var saveChanges = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("Save Changes?")
                                .PageSize(10)
                                .AddChoices("Yes", "No")
                            );

                            if(saveChanges == "Yes") {
                                var billToSave = new List<object>{billSelected};
                                FileHelper.SaveToFile2(billToSave);                                
                                Console.WriteLine($"Changes to Bill '{billSelected.Name}' has been Saved Successfully");
                            } 
                            else{
                                Console.WriteLine($"No Changes to Bill '{billSelected.Name}' were made");
                            }

                        }
                        else {
                            Console.WriteLine($"Bill '{billSelected.Name}' has been Marked as Payed as of {billSelected.DateModified}");
                            var billToSave = new List<object>{billSelected};
                            FileHelper.SaveToFile2(billToSave);                                
                            Console.WriteLine($"Changes to Bill '{billSelected.Name}' has been Saved Successfully");
                        }

                    }
                    else {
                        Console.WriteLine($"No Changes Made to Bill: {billSelected.Name}");
                    }
                }
            }

        }
    }
    static void addNewPriorities(string textFile) {
        List<string> result = new List<string>();
        string newID = DateTime.Now.Ticks.ToString();
        result.Add(newID);
        Console.Write("Add a new Priority  \n");
        Console.Write("Is this a new Task or Bill. \n");
        string priorityType = Console.ReadLine();
        result.Add(priorityType);
        Console.Write("Who shall we assign this to? Type Name: \n");
        string assignee = Console.ReadLine();
        result.Add(assignee);
        Console.Write("What is the name of this " + priorityType +"\n");
        string priorityName = Console.ReadLine();
        result.Add(priorityName);
        string dateCreated = DateTime.Now.ToString("dd-MM-yyyy");
        string dateModified = DateTime.Now.ToString("dd-MM-yyyy");
        result.Add(dateCreated);
        result.Add(dateModified);
        Console.Write("When is this due? Enter in dd-MM-YYYY format.\nExample: " + DateTime.Now.ToString("dd-MM-yyyy") +" dd = day, MM = Month, yyyy = Year\n");
        string dueDate = Console.ReadLine();
        result.Add(dueDate);
        string completed = "false";
        result.Add(completed);
        Console.Write("Add additonal Instructions " +priorityType+ "\n");
        string addInstructions = Console.ReadLine();
        result.Add(addInstructions);
        string placeHolderNote = "Notes ";
        result.Add(placeHolderNote);

        Console.WriteLine("\nConfirm the entry information");
        for(int i = 0;i < result.Count; i++) {
            if(i == 0) {
                Console.WriteLine("ID: " + result[i]);
            }
            else if( i == 1) {
                Console.WriteLine("Priority Type: " + result[i]);
            }
            else if( i == 2) {
                Console.WriteLine("Assigned To: " + result[i]);
            }
            else if( i == 3) {
                Console.WriteLine("Name: " + result[i]);
            }
            else if( i == 4) {
                Console.WriteLine("Date Created: " + result[i]);
            }
            else if( i == 5) {
                Console.WriteLine("Date Modified: " + result[i]);
            }
            else if( i == 6) {
                Console.WriteLine("Due Date: " + result[i]);
            }
            else if( i == 7) {
                Console.WriteLine("Task Completed: " + result[i]);
            }
            else if( i == 8) {
                Console.WriteLine("Instructions: " + result[i]);
            }
            else if( i == 9) {
                Console.WriteLine("Notes: " + result[i]);
            }
        }

        Console.WriteLine("\nIs the information Correct Y or N");
        string choice = Console.ReadLine();

        if (choice == "Y") {
            Console.WriteLine("Entry Information was saved to Textfile!");
            using (StreamWriter sw = new StreamWriter(textFile, append: true)) {
                sw.WriteLine(string.Join(",", result));
            }
        }
        else {
            Console.WriteLine("Entry was not save! Exiting...");
        }


    }

    static bool adminLogin(string login) {
        if(login == "admin") {
            return true;
        } else {
            return false;
        }
    }



}

