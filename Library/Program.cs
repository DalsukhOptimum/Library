using Library.Admin;
using Library.User;
using System.Data.SqlClient;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("select option\n1..Admin Login\n2..User Registration\n3..User Login\n");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //Admin login 
                        Console.WriteLine("Enter Your Email");
                        string Email = Console.ReadLine();
                        Console.WriteLine("Enter Your PassWord");
                        string Password = Console.ReadLine();
                        AdminDashBoard adminDashBoard = new AdminDashBoard();
                        adminDashBoard.AdminServiceDashBoard(Email, Password);
                        break;

                    case "2":
                        //User registration
                        Console.WriteLine("Enter Your Email");
                        string EmailRegister = Console.ReadLine();
                        Console.WriteLine("Enter Your PassWord");
                        string PasswordRegister = Console.ReadLine();
                        Console.WriteLine("Enter Your Name");
                        string name = Console.ReadLine();
                        UserRegister userregister = new UserRegister();
                       if(userregister.UserServiceLogin(EmailRegister, PasswordRegister,name))
                        {
                           Console.WriteLine("User Registered Successfully");
                        }
                        break;

                    case "3":
                        //user login 
                        Console.WriteLine("Enter Your Email");
                        string UserEmail = Console.ReadLine();
                        Console.WriteLine("Enter Your PassWord");
                        string UserPassword = Console.ReadLine();
                        UserDashBoard userDashBoard = new UserDashBoard();
                        userDashBoard.UserSericeDashBoard(UserEmail, UserPassword);
                        break;

                
                }

            }
            



        }
    }

}
