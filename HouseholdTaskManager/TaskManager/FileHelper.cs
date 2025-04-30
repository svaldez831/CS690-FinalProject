using System;
using System.IO;



namespace TaskManager;

public static class FileHelper {

    public static List<Bill> ReadBillsFromFile(string textFile){
        var bills = new List<Bill>();
        foreach (var row in File.ReadLines(textFile)) {
            var cols = row.Split(',');
            if(cols[1] == "Bill") {
                var bill = new Bill(cols[2], cols[3], cols[6],cols[8]);
                bill.Id = cols[0];
                bill.DateCreated = cols[4];
                bill.DateModified = cols[5];
                bill.DueDate = cols[6];
                bill.Paid = bool.TryParse(cols[7], out var paid) && paid;
                bill.Instructions = cols[8];
                bill.Notes = cols[9];
                bills.Add(bill);
            }
        }
        return bills;
    }


    public static List<Task> ReadTasksFromFile(string textFile){
        var tasks = new List<Task>();
        foreach (var row in File.ReadLines(textFile)) {
            var cols = row.Split(',');
            if(cols[1] == "Task") {
                var task = new Task(cols[2], cols[3], cols[6],cols[8]);
                task.Id = cols[0];
                task.DateCreated = cols[4];
                task.DateModified = cols[5];
                task.DueDate = cols[6];
                task.Completed = bool.TryParse(cols[7], out var complete) && complete;
                task.Instructions = cols[8];
                task.Notes = cols[9];
                tasks.Add(task);
            }
        }
        return tasks;
    }

    static void SaveBillToFile(List<Bill> bills) {
        string textFile = "records.txt";
        using (StreamWriter sw = new StreamWriter(textFile, append: true)) {
            foreach(var bill in bills) {
                sw.WriteLine($"{bill.Id}, {bill.Type},{bill.Assignee},{bill.Name},{bill.DateCreated},{bill.DateModified},{bill.DueDate},{bill.Paid},{bill.Instructions},{bill.Notes}");
            }
        }
    }

    public static Task ParseTask(List<string> cols) {

        if (cols.Count < 10 || cols[1] != "Task") 
            return null;

        var tempTask = new Task(cols[2], cols[3], cols[6], cols[8]);
        tempTask.Id = cols[0];
        tempTask.DateCreated = cols[4];
        tempTask.DateModified = cols[5];
        tempTask.DueDate = cols[6];
        tempTask.Completed = bool.TryParse(cols[7], out var completed) && completed;
        tempTask.Instructions = cols[8];
        tempTask.Notes = cols[9];

        return tempTask;
    }

    public static List<Task> ParseTasks(List<List<string>> rows) {
        var tempTasks = new List<Task>();
        foreach (var row in rows) {
            var task = ParseTask(row);
            if(task != null) {
                tempTasks.Add(task);
            }
        }
        return tempTasks;
    }




    public static List<List<string>> ReadCsvTo2Dlist(string textFile ){
        var result = new List<List<string>>();
    
        foreach(var line in File.ReadLines(textFile)) {
            var values = line.Split(',');
            result.Add(new List<string>(values));
        }
        return result;
    }

     public static List<string> GenerateUsers(string textFile) {
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

    public static List<string> GenerateUsers2(List<Task> tasks, List<Bill> bills) {
        List<string> users = new List<string>();

        foreach (Task task in tasks) {
            if(!users.Contains(task.Assignee)) {
                users.Add(task.Assignee);
            }
        }

        foreach (Bill bill in bills) {
            if(!users.Contains(bill.Assignee)) {
                users.Add(bill.Assignee);
            }
        }

        return users;
    }

    public  static void SaveToFile(List<string> data ) {
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






}
