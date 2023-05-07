using System;


namespace Test{
    class Program{
        static bool Login(Admin admin){
            Console.WriteLine("Who are you? : ");
            string name = Console.ReadLine();
            if(name != admin.name){
                Console.WriteLine("Enter password: ");
                string pass = Console.ReadLine();
                Console.WriteLine("Wrong username/password. Try again.");
                return false;
            }
            else{
                Console.WriteLine("Enter password: ");
                string pass = Console.ReadLine();
                if(!admin.checkPassword(pass)){
                    Console.WriteLine("Wrong username/password. Try again.");
                    return false;
                }
                else{
                    Console.WriteLine("Logged in as " + admin.name);
                    return true;
                }
            }
        }
        static void Main(String[] args){
            int loginAttempts = 0;
            User user1 = new User("Bob");
            User user2 = new User("Alice");
            User user3 = new User("John");
            User user4 = new User("Kate");
            User user5 = new User("Mike");
            User[] users = new User[6];
            users[0] = user1;
            users[1] = user2;
            users[2] = user3;
            users[3] = user4;
            users[4] = user5;
            Admin admin1 = new Admin(users, user1, "1234");
            // login
            
            // ask user1 to enter a command
            while(loginAttempts < 5){
                if(Login(admin1)){
                    break;
                }
                else{
                    loginAttempts++;
                }
            }
            if(loginAttempts == 5){
                Console.WriteLine("Too many login attempts. Exiting...");
                return;
            }
            while(true){
                Console.WriteLine("Enter command - type help for list of commands: ");
                string command = Console.ReadLine();
                admin1.enterCommand(command);
            }
        }
    }
    
    class User{
        public string name;
        public User(string name){
            this.name = name;
        }
        ~User(){
            Console.WriteLine("User deleted");
        }
    }
    class Admin : User{
        private string password;
        private User[] ofUsers;
        public Admin(User[] users, User admin, string password) : base(admin.name){
            this.ofUsers = users;
            this.password = password;
        }
        public bool checkPassword(string pass){
            if(pass != this.password){
                return false;
            }
            else{
                return true;
            }
        }
        public void enterCommand(string command){
            if(command == "delete"){
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                if(pass != this.password){
                    Console.WriteLine("Wrong password");
                    return;
                }
                Console.WriteLine("Enter user name");
                string userName = Console.ReadLine();
                DeleteUser(userName);
            }
            else if(command == "add"){
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                if(pass != this.password){
                    Console.WriteLine("Wrong password");
                    return;
                }
                Console.WriteLine("Enter user name");
                string userName = Console.ReadLine();
                AddUser(userName);
            }
            else if(command == "list"){
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                if(pass != this.password){
                    Console.WriteLine("Wrong password");
                    return;
                }
                ListUsers();
            }
            else if(command == "addspace"){
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                if(pass != this.password){
                    Console.WriteLine("Wrong password");
                    return;
                }
                Console.WriteLine("Enter number of users to add");
                int count = Convert.ToInt32(Console.ReadLine());
                AddSpace(count);
            }
            else if(command == "help"){
                Console.WriteLine("--------List of commands--------");
                Console.WriteLine("delete - delete user");
                Console.WriteLine("add - add user");
                Console.WriteLine("list - list users");
                Console.WriteLine("addspace - add space for users");
                Console.WriteLine("help - list commands");
                Console.WriteLine("-----------------------------");
            }
            else{
                Console.WriteLine("Command doesn't exist");
            }
        }
        private void DeleteUser(string userName){
            int count = 0;
            for(int i = 0; i < this.ofUsers.Length; i++){
                if(this.ofUsers[i] != null && this.ofUsers[i].name == userName){
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("User found with name " + userName + ", deleting...");
                    this.ofUsers[i] = null;
                    count++;
                }
            }
            if(count == 0){
                Console.WriteLine("No such user");
            }
            // list all users that exist now
            Console.WriteLine("--------New List of users--------");
            for(int i = 0; i < ofUsers.Length; i++){
                if(ofUsers[i] != null){
                    Console.WriteLine(ofUsers[i].name);
                }
            }
            Console.WriteLine("-----------------------------");
        }
        private void AddUser(string name){
            int count = 0;
            for(int i = 0; i < ofUsers.Length; i++){
                if(ofUsers[i] == null){
                    ofUsers[i] = new User(name);
                    count++;
                    break;
                }
            }
            if(count == 0){
                Console.WriteLine("No space for new user");
            }
            // list all users that exist now
            Console.WriteLine("--------New List of users--------");
            for(int i = 0; i < ofUsers.Length; i++){
                if(ofUsers[i] != null){
                    Console.WriteLine(ofUsers[i].name);
                }
            }
            Console.WriteLine("-----------------------------");
        }
        private void ListUsers(){
            Console.WriteLine("--------List of users--------");
            for(int i = 0; i < ofUsers.Length; i++){
                if(ofUsers[i] != null){
                    Console.WriteLine(ofUsers[i].name);
                }
            }
            Console.WriteLine("-----------------------------");
        }
        private void AddSpace(int count){
            User[] newUsers = new User[this.ofUsers.Length + count];
            for(int i = 0; i < this.ofUsers.Length; i++){
                newUsers[i] = this.ofUsers[i];
            }
            this.ofUsers = newUsers;
        }
        ~Admin(){
            Console.WriteLine("Admin deleted");
        }
    }
}