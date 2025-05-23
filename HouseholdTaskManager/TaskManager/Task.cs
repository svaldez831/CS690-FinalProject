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
        return $"| Task Name: {Name} | Description: {Instructions}  | Due: {DueDate}  | Last Modified: {DateModified} | Task Completed: {Completed} ";
    }

    public string DisplayAllString() {
        return $"|ID: {Id}\n|Task Name: {Name}\n|Description: {Instructions}\n|Assigned To: {Assignee}\n|Due: {DueDate}\n|Date Created: {DateCreated}\n|Last Modified: {DateModified}\n|Task Completed: {Completed}\n|Notes On Completion: {Notes}\n";
    }
        public string DisplayAllStringWithAlphabet() {
        return $"A) {Id}, B) {Name}, C) {Instructions}, D) {Assignee}, E) {DueDate}, F) {DateCreated}, G) {DateModified}, H) {Completed}, I) {Notes} ";
    }
    public string DisplayAllStringNoLables() {
        return $"{Id}| {Name}| {Instructions}| {Assignee}| {DueDate}| {DateCreated}| {DateModified}| {Completed}| {Notes}";
    }




}