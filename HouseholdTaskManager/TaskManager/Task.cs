namespace TaskManager;
public class Task {
    public string Id {get;set;}
    public string Type {get; set;}
    public string Assignee {get; set;}
    public string Name {get; set;}
    public string DateCreated {get; set;}
    public string DateModified{get; set;}
    public string DueDate {get; set;}
    public bool Completed {get; set;}
    public string Instructions {get; set;}
    public string Notes {get; set;}

    public Task (string assignee, string name, string dueDate, string instructions = " " ) {

        Id = DateTime.Now.Ticks.ToString();
        DateCreated = DateTime.Now.ToString("dd-MM-yyyy");
        DateModified = DateCreated;
        Assignee = assignee;
        Name = name;
        DueDate = dueDate;
        Completed = false;
        Instructions = instructions;
        Notes = "No notes.";
        Type = "Task";
    }

    public string ToDisplayString() {
        return $"| Assigned To: {Assignee} | Task ID: {Id} | Task Name: {Name} | Due: {DueDate}  | Modified: {DateModified} | Task Completed: {Completed} ";
    }





}