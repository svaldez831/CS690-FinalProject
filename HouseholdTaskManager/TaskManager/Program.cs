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
            var selectionOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please Select a User to Continue:")
                .PageSize(10)
                .AddChoices(userSelect)
            );
            if(selectionOption == "Admin") {
                Console.Clear();
                Console.WriteLine("Admin ModeActivated!");
            }
            else {
                Console.Clear();
                Console.WriteLine("USer ModeActivated!");
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


        // // var updateTask = new Task("Alice", "Clean the Toilets", "1-05-2025", "Fix ASAP" );
        // // updateTask.Id = "638812555262280090";
        // // updateTask.DateCreated = "30-04-2025";
        // // updateTask.DateModified = DateTime.Now.ToString("dd-MM-yyyy");
        // // updateTask.Completed = true;
        // // updateTask.Notes = updateTask.Name + " Cleaned all 3 toilets";
        // // var itemsToUpdate = new List<object> {updateTask};
        // // FileHelper.SaveToFile2(itemsToUpdate);

        // // Console.WriteLine("TaskUpdated");


        // // string mode = "";
        // // bool runProgram = true;
        // // bool backToMain = true;
        // // string selectedTask = "";
        // // List<string> modifiedTask = new List<string>();
        // do {
        //     // runProgram = true;
        //     // backToMain = true;
        //     // string choice = "";
        //     // Console.Write("Please select Mode (Admin or User) or END to exit Program\n");
        //     // mode = Console.ReadLine();
        //     // string textFile = "records.txt";
        //     // //List<string> users = FileHelper.GenerateUsers(textFile);
        //     // var tasks = FileHelper.ReadTasksFromFile(textFile);
        //     // var bill = FileHelper.ReadBillsFromFile(textFile);
        //     // var users = FileHelper.GenerateUsers2(tasks,bill);
        //     // Console.WriteLine("Users found: ");
        //     // foreach(var user2 in users) {
        //     //     Console.WriteLine(user2);
        //     // }
        //     //var aliceTask = RecordFilter.ExtractTaskByUser(tasks,"Dom");
        //     //Console.WriteLine($"Found{aliceTask.Count} tasks(s) for Dom");

        //     //var aliceBill= RecordFilter.ExtractBillByUser(bill,"Dom");
        //     //Console.WriteLine($"Found{aliceBill.Count} bills(s) for Dom");

        //     // Console.WriteLine($"Found {tasks.Count} task(s).");
        //     // Console.WriteLine($"Found {bill.Count} bill(s).");
        //     if(mode == "User") {
        //         // string user = selectUser(users);
        //         do {
        //             Console.Write("\n Welcome Back " + user +  "\n \n ");
        //             //var billsByUser = RecordFilter.ExtractByUser(bills, user);
        //             // tasksByUSer = RecordFilter.ExtractByUser(tasks, user);
        //             //Console.Write("You have " + tasksByUSer.Count + " task assigned to you. And " + billsByUser.Count + " upcoming bill you have to pay.\n\n");
        //             Console.Write("Enter [1] to see Tasks or  [2] for Upcoming Bills. or 'Back' to go back to \n");
        //             choice = Console.ReadLine();
        //             if(choice == "Back") {
        //                 backToMain = false;
        //             } 
        //             else if(choice == "1") {
        //             //         if(tasksByUSer.Count == 0) {
        //             //             Console.Write("0 Tasks are assigned to you\n");
        //             //         } 
        //             //         else {
        //             //             //printAllRecordsSimple(tasksByUSer);
        //             //             Console.Write("\n Please Choose a task to edit further \n");
        //             //             selectedTask = Console.ReadLine();
        //             //             int selectedTask1 = int.Parse(selectedTask);
        //             //             //List<string> modifiedRow = new List<string>( editSelectedTask( tasksByUSer[(selectedTask1 - 1)], "Task"));
        //             //             //modifiedTask = editSelectedTask(row);
        //             //             data = FileHelper.ReadCsvTo2Dlist(textFile);
        //             //             tasks = RecordFilter.ExtractTasks(data);
        //             //             //tasksByUSer = RecordFilter.ExtractByUser(tasks, user);
        //             //         }
            
        //             } 
        //             else if (choice == "2"){
        //                     // if(billsByUser.Count == 0) {
        //                     //     Console.Write("0 Bills are assigned to you");
        //                     // } 
        //                     // else {
        //                     //     printAllRecordsSimple(billsByUser);
        //                     //     Console.Write("\n Please Choose a Bill to edit further \n");
        //                     //     selectedTask = Console.ReadLine();
        //                     //     int selectedTask1 = int.Parse(selectedTask);
        //                     //     List<string> modifiedRow = new List<string>( editSelectedTask( billsByUser[(selectedTask1 - 1)], "Bill"));
        //                     //     //modifiedTask = editSelectedTask(row);
        //                     //     data = FileHelper.ReadCsvTo2Dlist(textFile);
        //                     //     bills = RecordFilter.ExtractTasks(data);
        //                     //     billsByUser = RecordFilter.ExtractByUser(bills, user);
        //                     // }
                       
        //                 }
        //             else {
        //                 Console.Write("Enter a Correct Option\n");
        //             }

        //         } while(backToMain);
        //     } 
        //     else if(mode == "Admin") {
        //         do {
        //             Console.Write("To login to the admin type the admin password: 'admin' or to go back to the Menu Type 'Back'\n");
        //             choice = Console.ReadLine();
        //             if(choice == "Back") {
        //                 backToMain = false;
        //             }
        //             else if (adminLogin(choice)){
        //                 Console.Write("WELCOME ADMIN\n");
        //                 addNewPriorities(textFile);
        //             }
        //             else {
        //                 Console.Write("Enter a Correct Option Try Again\n");
        //             }

        //         } while(backToMain);
        //     }
        //     else {
        //         runProgram = false;
        //     }

        // } while(runProgram);
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

