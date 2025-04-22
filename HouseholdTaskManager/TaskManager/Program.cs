namespace TaskManager;

class Program
{
    static void Main(string[] args) { 
        bool runProgram = true;
        string choice;
        Console.Write("Please select Mode (Admin or User)\n");
        string mode = Console.ReadLine();
        List<string> users = new List<string> { "Sam", "JohnCena", "Dom" };

        if(mode == "User") {
            string user = selectUser(users);
            do {
                
                Console.Write("HELLO " + user +  " OR 'END' TO EXIT\n");
                choice = Console.ReadLine();
                if(choice == "END") {
                    runProgram = false;
                }
                else {

                }
                
            } while(runProgram);
        } else {
            do {
                Console.Write("Hello ADMIN Select AN OPTION OR 'END' TO EXIT\n");
                choice = Console.ReadLine();
                if(choice == "END") {
                    runProgram = false;
                }
                else {

                }

            } while(runProgram);
        }
    }
    static string selectUser(List<string> userList) {
        string choice;
        Console.Write("Who Are you?\n");
        for (int i = 0; i < userList.Count; i ++) {
            Console.Write("Press " +  (i+1) + " for "+ userList[i] + "\n");
        }
        int user = Int32.Parse(Console.ReadLine());
        if(user == 1){
            choice = userList[0];
        } else if(user == 2) {
            choice = userList[1];
        } else {
            choice = userList[2];
        }

        return choice;
    }




}


