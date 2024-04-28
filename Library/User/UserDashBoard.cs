using Library.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Classes;

namespace Library.User
{
    public class UserDashBoard:ConnectionLogic
    {
        int UserId;
          public void UserSericeDashBoard(string Email,string Password)
        {
            try
            {
               
                con.Open();

                //checking whether user is exists or not 
                SqlCommand queryUserFind = new SqlCommand("IsAvailable", con);
                queryUserFind.CommandType = System.Data.CommandType.StoredProcedure;
                queryUserFind.Parameters.AddWithValue("@Email", Email);
                queryUserFind.Parameters.AddWithValue("@Password", Password);
                queryUserFind.Parameters.AddWithValue("@Name", "");
                queryUserFind.Parameters.AddWithValue("@OperationType", "UserExistence");
                SqlParameter Id = queryUserFind.Parameters.AddWithValue("@Result", SqlDbType.Int);
                SqlParameter Count = queryUserFind.Parameters.AddWithValue("@Count", SqlDbType.Int);
                Id.Direction = ParameterDirection.Output;
                Count.Direction = ParameterDirection.Output;
                
                queryUserFind.ExecuteNonQuery();

               
                //Console.WriteLine(Count.Value);
                if ((int)Count.Value == 0)
                {
                    Console.WriteLine("you are not an User");
                  
                    return;
                }
                else
                {
                    UserId = (int)Id.Value;
                }

                con.Close();
                



                while (true)
                {
                    Console.WriteLine("1.. Want to Borrow Book\n2..want to Release Book\n3. See All the Books Which You have\n4.return");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                         
                        case "1":
                            //borrow Book
                            UserBorrow userBorrow = new UserBorrow();
                            userBorrow.BorrowBook(UserId);

                            break;

                        case "2":
                            //return book
                            UserReturn userReturn = new UserReturn();
                            userReturn.ReturnBook(UserId);
                            break;

                        case "3":
                            //show books which that partivcular user have 
                             UserAllBooks userAllBooks = new UserAllBooks();
                            userAllBooks.ShowAllBooks(UserId);
                            break;

                        case "4":
                            //return to the login ore register page 
                            return;

                    }
                }
              
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.PrintingError(ex.Message, "UserDashBoard");
                ;
            }
        }
    }
        

}
