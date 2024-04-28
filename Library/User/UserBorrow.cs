using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task.Classes;

namespace Library.User
{
    public class UserBorrow:ConnectionLogic
    {
        

        //making entry in both bokkborrowhistory and book table from same stored procedure
        public bool BookEntry(int UserId , int BookId, string OperationType)
        {
            try
            {
                 
                SqlCommand queryUpdateBook = new SqlCommand("BorrowHistoryEntry", con);
                queryUpdateBook.CommandType = System.Data.CommandType.StoredProcedure;
                queryUpdateBook.Parameters.AddWithValue("@UserId", UserId);
                queryUpdateBook.Parameters.AddWithValue("@BookId", BookId);
                queryUpdateBook.Parameters.AddWithValue("@OperationType", OperationType);

                queryUpdateBook.ExecuteNonQuery();
            
                return true;

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return false;
            }
        }
        public void BorrowBook(int UserId)
        {
            try
            {
                con.Open();

                int cnt = 0;
                //showing all the books which is available means available book is greater than zero
                SqlCommand queryBookFind = new SqlCommand("UserBooks", con);
                queryBookFind.CommandType = System.Data.CommandType.StoredProcedure;
                queryBookFind.Parameters.AddWithValue("@UserId", UserId);
                queryBookFind.Parameters.AddWithValue("@OperationType", "AvailableForUser");
                SqlDataReader dataReaderBookFind = queryBookFind.ExecuteReader();

                Console.WriteLine("Enter the  BookId which you want to Borrow");
              
                while (dataReaderBookFind.Read())
                {
                    cnt++;
                    Console.WriteLine("BookId: " + dataReaderBookFind["Id"] + " " + "BookTitle:" + dataReaderBookFind["Title"]);
                }
                if(cnt == 0)
                {
                    Console.WriteLine("there is no books Available");
                    return;
                }
                dataReaderBookFind.Close();
       
                int BookId = Convert.ToInt32(Console.ReadLine());
             
                //making entry in both bookborrowhistory and book table through function
                bool flag = BookEntry(UserId, BookId, "BookTableEntry");
                if (!flag)
                {
                    Console.WriteLine("something went wrong please try again");
                    return;
                }
                flag = BookEntry(UserId, BookId, "BorrowHistory");
                if (!flag)
                {
                    Console.WriteLine("something went wrong please try again");
                    return;
                }


                Console.WriteLine("Book Borrowed Sucessfully");
                con.Close();

            }
            catch(Exception ex)
            {
                Error.PrintingError(ex.Message, "UserBorrowBook");
                Console.WriteLine(ex.Message);
             
                con.Close();
            }

           

        }
    }
}
