namespace HouseHoldTaskManager.Tests;
using TaskManager;
public class FileHelperTests {
    

    [Fact]
    public void ReadTasksFromFileTestCount()
    {
        string testFile = "test_records.txt";
        File.WriteAllLines(testFile, new[] {
            "1,Task,Dom,Take out the Trash,22-04-2025,01-05-2025,26-04-2025,True,Please take out of the trash from each room including the restrooms,Took it out, but need to buy garbage bags.",
            "2,Task,JohnCena,Rake Leaves,22-04-2025,23-04-2025,22-04-2025,false,Rake Leaves and empty in trash bin,Notes"
        });

        var tasks = FileHelper.ReadTasksFromFile(testFile);

        Assert.Equal(2, tasks.Count);

    }

    [Fact]
    public void SaveToFile2UpdateExistingTask() {
        string testFile = "test_records.txt";
        File.WriteAllLines(testFile, new[] {
            "1,Task,Dom,Take out the Trash,22-04-2025,01-05-2025,26-04-2025,true,Please take out of the trash from each room including the restrooms,Took it out but need to buy garbage bags.",
        });

        var task = new Task("","","","") {
            Id = "1",
            Type = "Task",
            Assignee = "Dom",
            Name = "Take out the Trash",
            DateCreated = "22-5-2025",
            DateModified = "22-5-2025",
            DueDate = "22-5-2025",
            Completed = true, 
            Instructions = "Only take out Trash in kitchen",
            Notes = "Updated Note"
        };
        FileHelper.SaveToFile2(new List<object> {task}, testFile);
        var textLines = FileHelper.ReadTasksFromFile(testFile);
        var lines = File.ReadAllLines(testFile);
        Assert.Single(textLines);
        Assert.Contains("Updated Note", lines[0]);
    }


}