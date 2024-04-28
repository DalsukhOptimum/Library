using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using Library.Admin;
using Task.Classes;

namespace Library.User
{
    public class UserAllBooks:ConnectionLogic
    {
       
        public void ShowAllBooks(int userId)
        {
            try
            {
                con.Open();
                int cnt = 0;
              
                //showing books which user have (Borrowed)
                SqlCommand queryFindBook = new SqlCommand("UserBooks", con);
                queryFindBook.CommandType = System.Data.CommandType.StoredProcedure;
                queryFindBook.Parameters.AddWithValue("@UserId", userId);
                queryFindBook.Parameters.AddWithValue("@OperationType", "UserBooks");
                SqlDataReader dataReaderBookTable = queryFindBook.ExecuteReader();
                bool flag = false;
                while (dataReaderBookTable.Read())
                {
                    Console.WriteLine("BookId: " + dataReaderBookTable["Bookid"] + " " + "BookTitle:" + dataReaderBookTable["Title"] + " " + "Book Edition:" + dataReaderBookTable["Edition"]);
                    cnt++;
                }
                dataReaderBookTable.Close();
                //if count is zero means user don't have any borrowed books
                if (cnt == 0)
                {
                    Console.WriteLine("You Don't Have Any Books");
                    return;
                }

            }
            catch(Exception ex)
            {
                Error.PrintingError(ex.Message, "UserAllBooks");
            }
           
        }
    }
}
