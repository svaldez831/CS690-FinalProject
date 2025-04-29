using System;
using System.IO;



namespace TaskManager;

public static class FileHelper {
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
