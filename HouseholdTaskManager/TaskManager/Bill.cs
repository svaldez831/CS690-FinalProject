namespace TaskManager;
public class Bill {
    public string Id {get;set;}
    public string Type {get; set;}
    public string Assignee {get; set;}
    public string Name {get; set;}
    public string DateCreated {get; set;}
    public string DateModified {get; set;}
    public string DueDate {get; set;}
    public bool Paid {get; set;}
    public string Instructions {get; set;}
    public string Notes {get; set;}

    public Bill (string assignee, string name, string dueDate, string instructions = " " ) {

        Id = DateTime.Now.Ticks.ToString();
        DateCreated = DateTime.Now.ToString("dd-MM-yyyy");
        DateModified = DateCreated;
        Assignee = assignee;
        Name = name;
        DueDate = dueDate;
        Paid = false;
        Instructions = instructions;
        Notes = "No notes.";
        Type = "Bill";
    }

    public string ToDisplayString() {
        return $"| Bill Name: {Name} | Instructions: {Instructions} | Due: {DueDate} | Last Modified: {DateModified} | Bill Paid: {Paid} ";
    }





}