namespace TaskManager;
using System;
using System.IO;
public static class RecordFilter {
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

    public static List<Task> ExtractTasksCompleted(List<Task> tasks){
        List<Task> newList = new List<Task>();
        foreach(Task task in tasks) {
        if (task.Completed) {
                newList.Add(task);
            }
        }

        return newList;
    }
    
}