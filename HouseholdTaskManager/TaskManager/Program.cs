namespace TaskManager;
using System;//writeline
using System.Collections.Generic;//collections
using System.IO;//readlines

class Program
{
    static void Main(string[] args) { 
        string mode = "";
        bool runProgram = true;
        bool backToMain = true;
        string selectedTask = "";
        List<string> modifiedTask = new List<string>();
        do {
            runProgram = true;
            backToMain = true;
            string choice = "";
            Console.Write("Please select Mode (Admin or User) or END to exit Program\n");
            mode = Console.ReadLine();
            string textFile = "records.txt";
            List<string> users = generateUsers(textFile);
            var data = readCsvTo2Dlist(textFile);
            var bills = extractBills(data);
            var tasks = extractTasks(data);
       
            if(mode == "User") {
                string user = selectUser(users);
                do {
                    Console.Write("\n Welcome Back " + user +  "\n \n ");
                    var billsByUser = extractByUser(bills, user);
                    var tasksByUSer = extractByUser(tasks, user);
                    Console.Write("You have " + tasksByUSer.Count + " task assigned to you. And " + billsByUser.Count + " upcoming bill you have to pay.\n\n");
                    Console.Write("Enter [1] to see Tasks or  [2] for Upcoming Bills. or 'Back' to go back to \n");
                    choice = Console.ReadLine();
                    if(choice == "Back") {
                        backToMain = false;
                    } 
                    else if(choice == "1") {
                            if(tasksByUSer.Count == 0) {
                                Console.Write("0 Tasks are assigned to you\n");
                            } 
                            else {
                                printAllRecordsSimple(tasksByUSer);
                                Console.Write("\n Please Choose a task to edit further \n");
                                selectedTask = Console.ReadLine();
                                int selectedTask1 = int.Parse(selectedTask);
                                List<string> modifiedRow = new List<string>( editSelectedTask( tasksByUSer[(selectedTask1 - 1)], "Task"));
                                //modifiedTask = editSelectedTask(row);
                                data = readCsvTo2Dlist(textFile);
                                tasks = extractTasks(data);
                                tasksByUSer = extractByUser(tasks, user);
                            }
            
                    } 
                    else if (choice == "2"){
                            if(billsByUser.Count == 0) {
                                Console.Write("0 Bills are assigned to you");
                            } 
                            else {
                                printAllRecordsSimple(billsByUser);
                                Console.Write("\n Please Choose a Bill to edit further \n");
                                selectedTask = Console.ReadLine();
                                int selectedTask1 = int.Parse(selectedTask);
                                List<string> modifiedRow = new List<string>( editSelectedTask( billsByUser[(selectedTask1 - 1)], "Bill"));
                                //modifiedTask = editSelectedTask(row);
                                data = readCsvTo2Dlist(textFile);
                                bills = extractTasks(data);
                                billsByUser = extractByUser(bills, user);
                            }
                       
                        }
                    else {
                        Console.Write("Enter a Correct Option\n");
                    }

                } while(backToMain);
            } 
            else if(mode == "Admin") {
                do {
                    Console.Write("To login to the admin type the admin password: 'admin' or to go back to the Menu Type 'Back'\n");
                    choice = Console.ReadLine();
                    if(choice == "Back") {
                        backToMain = false;
                    }
                    else if (adminLogin(choice)){
                        Console.Write("WELCOME ADMIN\n");
                        addNewPriorities(textFile);
                    }
                    else {
                        Console.Write("Enter a Correct Option Try Again\n");
                    }

                } while(backToMain);
            }
            else {
                runProgram = false;
            }

        } while(runProgram);
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
    static List<string> generateUsers(string textFile) {
        List<string> lines = File.ReadLines("records.txt").ToList();
        List<string> users = new List<string>();
        for(int i = 0; i < lines.Count; i++) {
            string[] columns = lines[i].Split(',');

            if(!users.Contains(columns[2])) {
                users.Add(columns[2]);
            }
        }

        return users;
    }

    static void saveToFile(List<string> data ) {
        List<string> lines = File.ReadLines("records.txt").ToList();
       
       for(int i = 0; i < lines.Count; i++) {
        string[] columns = lines[i].Split(',');

        if(columns[0].Trim() == data[0].Trim()) {
            columns[1] = data[1];
            columns[2] = data[2];
            columns[3] = data[3];
            columns[4] = data[4];
            columns[5] = data[5];
            columns[6] = data[6];
            columns[7] = data[7];
            columns[8] = data[8];
            columns[9] = data[9];

            lines[i] = string.Join(",", columns);
            break;
        }
       
       }
        

       
        File.WriteAllLines("records.txt",lines);

    }
    static List<string>  editSelectedTask ( List<string> data, string type) {
        bool save = false;
        string change = "";
        string addNote = "";
        string note = " "+ data[2] + ": ";
        string letsSave = "";
      while(!save) {
        if(type == "Task") {
            Console.Write(data[3] + "\n Have you finished this task? Yes or No");
        }
        else {
            Console.Write(data[3] + "\n Have you Paid this Bill? Yes or No");
        }
        
        change = Console.ReadLine();
        if(change == "Yes") {

            string lastModified = DateTime.Now.ToString("dd-MM-yyyy");
            if(type == "Task") {
                Console.Write("Task Changed to Complete, Last Modified:"+ lastModified + "\n");
            } 
            else{
                Console.Write("Bill Paid, Last Modified:"+ lastModified + "\n");

            }
            data[7] = "true";
            data[5] =lastModified;
            data[6] = lastModified;
        }
        Console.Write("Would you like to add a note? Start Typing if not type 'No' \n");
        addNote = Console.ReadLine();

        if(note == "No") {
            data.Add(note + "No Note.");
        }
        else {
            if(data.Count < 10) {
                data.Add(note + addNote);
            }
            else{
                data[9] += note + addNote;
            }
            
        }
        Console.Write("Would you like to save? Yes and Exit or Change Something else?\n");
        letsSave = Console.ReadLine();

        if(letsSave == "Yes") {
            save = true;
            saveToFile(data);
        }

      }
        //Console.WriteLine(string.Join(", ", data));

        
        return data;
    }

    static List<List<string>> extractBills (List<List<string>> data) {
        var result = new List<List<string>>();
        string keyword = "Bill";
        foreach (var row in data) {
            if (row.Count > 1 && row[1] == keyword) {
                result.Add(new List<string>(row));
                
            }
        }

        
        return result;

    } 

        static List<List<string>> extractByUser (List<List<string>> data, string user) {
        var result = new List<List<string>>();
        string keyword = user;
        
        foreach (var row in data) {
            if (row.Count > 1 && row[2] == keyword) {
                result.Add(new List<string>(row));
                
            }
        }

        
        return result;

    } 

    
    static List<List<string>> extractTasks (List<List<string>> data) {
        var result = new List<List<string>>();
        string keyword = "Task";
        foreach (var row in data) {
            if (row.Count > 1 && row[1] == keyword) {
                result.Add(new List<string>(row));
            }
        }

        
        return result;
    } 
    static string selectUser(List<string> userList) {
        string choice ="";
        Console.Write("Who Are you?\n");
        for (int i = 0; i < userList.Count; i ++) {
            Console.Write("Press " +  (i+1) + " for "+ userList[i] + "\n");
        }
        int user = Int32.Parse(Console.ReadLine());
        if(user >= 1 && user <= userList.Count) {
            choice = userList[user-1];
        } 
        else {
            choice = userList[2];
        }

        return choice;
    }

    static void printTasksByUser(string user) {
            //foreach (var row in result) {
            //Console.WriteLine(string.Join(" | ", row));
        //}
    }

    static void printAllRecordsSimple(List<List<string>> data) {
        int counter = 1;
        foreach(var row in data) {
            Console.WriteLine(counter +") "+ string.Join(" | ", row));
            counter++;
        }
        Console.Write("\n");
    }

    static List<List<string>> readCsvTo2Dlist(string textFile ){
        var result = new List<List<string>>();
        
        foreach(var line in File.ReadLines(textFile)) {
            var values = line.Split(',');
            result.Add(new List<string>(values));
        }
        return result;
    }


}

class User {
    public string Name { get; private set;}

    public User(string name) {
        Name = name;
    }

    static string selectUser(List<string> userList) {
        
        Console.Write("Who Are you?\n");
        for (int i = 0; i < userList.Count; i ++) {
            Console.Write("Press " +  (i+1) + " for "+ userList[i] + "\n");
        }
        int user = Int32.Parse(Console.ReadLine());
        if(user >= 1 && user <= userList.Count) {
            choice = userList[user-1];
        } 
        else {
            choice = userList[2];
        }

        return choice;
    }

}


