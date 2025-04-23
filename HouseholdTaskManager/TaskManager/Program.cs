namespace TaskManager;
using System;//writeline
using System.Collections.Generic;//collections
using System.IO;//readlines

class Program
{
    static void Main(string[] args) { 
        bool runProgram = true;
        string choice;
        Console.Write("Please select Mode (Admin or User)\n");
        string mode = Console.ReadLine();
        List<string> users = new List<string> { "Sam", "JohnCena", "Dom" };
        string textFile = "records.txt";
        var data = readCsvTo2Dlist(textFile);
        var bills = extractBills(data);
        var tasks = extractTasks(data);
        // foreach(var row in data) {
        //     Console.WriteLine(string.Join(" | ", row));
        // }
        //Console.WriteLine(text);
        if(mode == "User") {
            string user = selectUser(users);
            do {
                
                Console.Write("Welcome Back " + user +  "\n  ");
                
                choice = Console.ReadLine();
                if(choice == "END") {
                    runProgram = false;
                }
                else {
                    printAllRecordsSimple(data);
                }
                
            } while(runProgram);
        } else {
            do {
                Console.Write("Hello ADMIN Select AN OPTION OR 'END' TO EXIT\n");
                choice = Console.ReadLine();
                if(choice == "END") {
                    runProgram = false;
                }
                else {

                }

            } while(runProgram);
        }
    }

    static List<List<string>> extractBills (List<List<string>> data) {
         var result = new List<List<string>>();
        string keyword = "Bill";
        foreach (var row in data) {
            if (row.Count > 1 && row[1] == keyword) {
                result.Add(new List<string>(row));
            }
        }
        foreach (var row in result) {
            Console.WriteLine(string.Join(" | ", row));
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
        foreach (var row in result) {
            Console.WriteLine(string.Join(" | ", row));
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
        if(user == 1){
            choice = userList[0];
        } else if(user == 2) {
            choice = userList[1];
        } else {
            choice = userList[2];
        }

        return choice;
    }

    static void printTasksByUser(string user) {

    }

    static void printAllRecordsSimple(List<List<string>> data) {
        foreach(var row in data) {
             Console.WriteLine(string.Join(" | ", row));
        }
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


