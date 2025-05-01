namespace TaskManager;

public static class AdminActions {
    public static void ViewAllTasks() {
        Console.Clear();
        var tasks = FileHelper.ReadTasksFromFile("records.txt");
        Console.WriteLine("== All Tasks ==");
        Console.WriteLine("A) ID | B) Task Name | C) Description | D) Assigned To | E) Due | F) Date Created | G) Last Modified | H) Task Completed |I) Notes On Completion");
        foreach(var task in tasks) {
            Console.WriteLine("\n" + task.DisplayAllStringWithAlphabet());
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

        public static void ViewAllBills() {
        Console.Clear();
        var bills = FileHelper.ReadBillsFromFile("records.txt");
        Console.WriteLine("== All Bills ==");
        Console.WriteLine("A) ID | B) Bill Name | C) Description | D) Assigned To | E) Due | F) Date Created | G) Last Modified | H) Bill Paid |I) Notes On Completion");
        foreach(var bill in bills) {
            Console.WriteLine("\n" + bill.DisplayAllStringWithAlphabet());
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    
}