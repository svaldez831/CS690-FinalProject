namespace TaskManager;
using System;
using System.IO;
public static class RecordFilter {

    

    public static List<List<string>> ExtractTasks (List<List<string>> data) {
        var result = new List<List<string>>();
        string keyword = "Task";
        foreach (var row in data) {
            if (row.Count > 1 && row[1] == keyword) {
                result.Add(new List<string>(row));
            }
        }

        
        return result;
    } 

    public static List<Bill> ExtractBills (List<List<string>> data) {
        var result = new List<Bill>();
        string keyword = "Bill";
        foreach (var row in data) {
            if (row.Count > 1 && row[1] == keyword) {
                var bill = new Bill(row[2], row[3], row[6],row[8]);
                bill.Id = row[0];
                bill.DateCreated = row[4];
                bill.DateModified = row[5];
                bill.DueDate = row[6];
                bill.Paid = bool.TryParse(row[7], out var paid) && paid;
                bill.Instructions = row[8];
                bill.Notes = row[9];
                result.Add(bill);
            }
        }

        
        return result;

    }

    public static List<List<string>> ExtractByUser (List<List<string>> data, string user) {
        var result = new List<List<string>>();
        string keyword = user;
        
        foreach (var row in data) {
            if (row.Count > 1 && row[2] == keyword) {
                result.Add(new List<string>(row));
                
            }
        }

        
        return result;

    } 

    public static List<Task> ExtractTaskByUser(List<Task> tasks, string user) {
        List<Task> result = new List<Task>();
        foreach(Task task in tasks) {
            if (task.Assignee == user) {
                result.Add(task);
            }
        }
        return result;
    }
    public static List<Bill> ExtractBillByUser(List<Bill> bills, string user) {
        List<Bill> result = new List<Bill>();
        foreach(Bill bill in bills) {
            if (bill.Assignee == user) {
                result.Add(bill);
            }
        }
        return result;
    }
    
}