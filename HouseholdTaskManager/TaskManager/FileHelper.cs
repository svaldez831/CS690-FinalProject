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


    public static void SaveToFile2(List<object> items) {
        string filePath = "records.txt";
        List<string> rows = File.ReadAllLines(filePath).ToList();

        foreach (var item in items) {
            string itemId = "";
            string itemType = "";
            string[] newCols = new string[10];

            if(item is Task task) {
                itemId = task.Id;
                itemType = "Task";
                newCols[0] = task.Id;
                newCols[1] = task.Type;
                newCols[2] = task.Assignee;
                newCols[3] = task.Name;
                newCols[4] = task.DateCreated;
                newCols[5] = task.DateModified;
                newCols[6] = task.DueDate;
                newCols[7] = task.Completed.ToString();
                newCols[8] = task.Instructions;
                newCols[9] = task.Notes;
            }
             else if(item is Bill bill) {
                itemId = bill.Id;
                itemType = "Bill";
                newCols[0] = bill.Id;
                newCols[1] = bill.Type;
                newCols[2] = bill.Assignee;
                newCols[3] = bill.Name;
                newCols[4] = bill.DateCreated;
                newCols[5] = bill.DateModified;
                newCols[6] = bill.DueDate;
                newCols[7] = bill.Paid.ToString();
                newCols[8] = bill.Instructions;
                newCols[9] = bill.Notes;
            }
            for(int i = 0; i < rows.Count; i++) {
                string[] columns = rows[i].Split(',');
                if(columns[0].Trim() == itemId) {
                    rows[i] = string.Join(",", newCols);
                    break;
                }
            }
        }

        File.WriteAllLines(filePath, rows);
    }


}
