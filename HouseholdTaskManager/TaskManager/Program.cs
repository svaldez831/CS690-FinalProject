namespace TaskManager;

class Program
{
    static void Main(string[] args) { 
        bool runProgram = true;
        string choice;
        Console.Write("Please select Mode (Admin or User)");
        string mode = Console.ReadLine();

        if(mode == "User") {
            do {
                Console.Write("SELECT AN OPTION OR 'END' TO EXIT");
                choice = Console.ReadLine();
                if(choice == "END") {
                    runProgram = false;
                }
                else {

                }
                
            } while(runProgram);
        } else {
            do {
                Console.Write("Hello ADMIN Select AN OPTION OR 'END' TO EXIT");
                choice = Console.ReadLine();
                if(choice == "END") {
                    runProgram = false;
                }
                else {

                }

            } while(runProgram);
        }
    }
        
}
