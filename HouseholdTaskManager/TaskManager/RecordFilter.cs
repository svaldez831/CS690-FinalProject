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

    public static List<List<string>> ExtractBills (List<List<string>> data) {
        var result = new List<List<string>>();
        string keyword = "Bill";
        foreach (var row in data) {
            if (row.Count > 1 && row[1] == keyword) {
                result.Add(new List<string>(row));
                
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

}