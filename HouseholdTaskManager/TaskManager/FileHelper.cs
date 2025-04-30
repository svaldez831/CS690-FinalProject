using System;
using System.IO;



namespace TaskManager;

public static class FileHelper {

    public static Task ParseTask(List<string> cols) {

        if (cols.Count < 10 || cols[1] != "Task") 
            return null;

        var tempTask = new Task(cols[2], cols[3], cols[6], cols[8]);
        tempTask.Id = cols[0];
        tempTask.DateCreated = cols[4];
        tempTask.DateModified = cols[4];
        tempTask.Assignee =cols[4];
        tempTask.Name = cols[4];
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
