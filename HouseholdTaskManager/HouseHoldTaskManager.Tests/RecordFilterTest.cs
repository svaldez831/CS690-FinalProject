namespace HouseHoldTaskManager.Tests;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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




}