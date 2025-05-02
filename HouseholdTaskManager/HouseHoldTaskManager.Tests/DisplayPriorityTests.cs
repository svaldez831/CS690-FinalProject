namespace HouseHoldTaskManager.Tests;

using TaskManager;
public class DisplayPriorityTests {
    [Fact]
    public void TaskToDisplayReturnsCorrectFormat() {
        var task = new Task("Sam", "task1", "04-10-2025", "Take out"){
            Completed = true,
            DueDate = "10-1-2025",
            Notes = "Reminder"
        };

        var display = task.ToDisplayString();
        Assert.Contains("task1", display);
        Assert.Contains("Take out",display);
    }

        [Fact]
    public void BillToDisplayReturnsCorrectFormat() {
        var bill = new Bill("Sam", "task1", "04-10-2025", "Take out"){
            Paid = true,
            DueDate = "10-1-2025",
            Notes = "Reminder"
        };

        var display = bill.DisplayAllString();
        Assert.Contains("Sam",display);
        Assert.Contains("Take out",display);
    }

}