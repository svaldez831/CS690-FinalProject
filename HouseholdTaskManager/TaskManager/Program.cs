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
}


