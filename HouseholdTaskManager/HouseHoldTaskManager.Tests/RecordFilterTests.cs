namespace HouseHoldTaskManager.Tests;

using TaskManager;
public class RecordFilterTests {
    
    
    [Fact]
    public void ExtractTaskByUserReturnOnlyMatch() {
        var tasks = new List<Task> {
            new Task ("Alice", "Task1", "05-6-2025", "task1"),
            new Task ("Bob", "Task2", "05-6-2025", "task2"),
        };
        var user = RecordFilter.ExtractTaskByUser(tasks, "Alice");
        Assert.Single(user);
        Assert.Equal("Task1", user[0].Name);
    }

    [Fact]
    public void ExtractBillsByUserReturnOnlyMatch() {
        var bills = new List<Bill> {
            new Bill ("Alice", "Bill1", "05-6-2025", "Pay"),
            new Bill ("Bob", "Bill2", "05-6-2025", "Paymore"),
        };
        var user = RecordFilter.ExtractBillByUser(bills, "Alice");
        Assert.Single(user);
        Assert.Equal("Bill1", user[0].Name);
    }

    [Fact]
    public void ExtractTasksCompletedReturnCompletedOnly() {
        var tasks = new List<Task> {
            new Task ("Alice", "Task1", "05-6-2025", "task1"),
            new Task ("Bob", "Task2", "05-6-2025", "task2"),
            new Task ("Sam", "Task1", "05-6-2025", "task3"),
            new Task ("Chuy", "Task2", "05-6-2025", "task4"),
        };
        tasks[0].Completed = true;
        tasks[3].Completed = true;
       var extracted =  RecordFilter.ExtractTasksCompleted(tasks);
       Assert.Equal(2, extracted.Count);
       Assert.True( extracted[0].Completed);
       Assert.True( extracted[1].Completed);
     
    }
    



}