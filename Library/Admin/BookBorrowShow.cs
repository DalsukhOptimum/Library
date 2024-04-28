using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Classes;

namespace Library.Admin
{
    public class BookBorrowShow:ConnectionLogic
    {
        

       public void ShowBorrowBooks()
        {
            try
            {
                con.Open();
                //showing all the borrowed books from all the users 
                SqlCommand queryBookFind = new SqlCommand("BookOwnShow", con);
                queryBookFind.CommandType = System.Data.CommandType.StoredProcedure;
                queryBookFind.ExecuteNonQuery();
                SqlDataReader dataReaderBookTable = queryBookFind.ExecuteReader();
                bool flag = false;
                while (dataReaderBookTable.Read())
                {
                    Console.WriteLine("Id " + dataReaderBookTable["Id"] + " " + "BookTitle:" + dataReaderBookTable["Title"] + " " + "Book Edition:" + dataReaderBookTable["Edition"] + " Name: " + dataReaderBookTable["Name"]);

                }
                dataReaderBookTable.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.PrintingError(ex.Message, "BookBorrowShow");
            }
           
        }
    }
}
