namespace TaskManager;
using System;//writeline
using System.Collections.Generic;//collections
using System.IO;//readlines

class Program
{
    static void Main(string[] args) { 
        string mode;
        bool runProgram = true;
        bool backToMain = true;
        do {
            runProgram = true;
            backToMain = true;
            string choice;
            Console.Write("Please select Mode (Admin or User) or END to exit Program\n");
            mode = Console.ReadLine();
            List<string> users = new List<string> { "Sam", "JohnCena", "Dom" };
            string textFile = "records.txt";
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
                                Console.Write("0 Tasks are assigned to you");
                            } 
                            else {
                                printAllRecordsSimple(tasksByUSer);
                            }
            
                    } 
                    else if (choice == "2"){
                            if(billsByUser.Count == 0) {
                                Console.Write("0 Bills are assigned to you");
                            } 
                            else {
                                printAllRecordsSimple(billsByUser);
                            }
                       
                        }
                    else {
                        Console.Write("Enter a Correct Option\n");
                    }

                } while(backToMain);
            } 
            else if(mode == "Admin") {
                do {
                    Console.Write("Hello ADMIN Select AN OPTION OR 'Back' to go Back to Main Menu\n");
                    choice = Console.ReadLine();
                    if(choice == "END") {
                        backToMain = false;
                    }
                    else {

                    }

                } while(backToMain);
            }
            else {
                runProgram = false;
            }

        } while(runProgram);
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
        string choice;
        Console.Write("Who Are you?\n");
        for (int i = 0; i < userList.Count; i ++) {
            Console.Write("Press " +  (i+1) + " for "+ userList[i] + "\n");
        }
        int user = Int32.Parse(Console.ReadLine());
        if(user == 1) {
            choice = userList[0];
        } 
        else if(user == 2) {
            choice = userList[1];
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
        foreach(var row in data) {
            Console.WriteLine(string.Join(" | ", row));
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


