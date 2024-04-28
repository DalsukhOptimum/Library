using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Admin
{
    public class DeleteBook:ConnectionLogic
    {
       
       
        public void AdminServiceDeleteBook(int Adminid)
        {


            con.Open();

            try
            {
                //showing all the books to the user 
                SqlCommand queryBookFind = new SqlCommand("BookSelectOrDelete", con);
                queryBookFind.CommandType = System.Data.CommandType.StoredProcedure;
                queryBookFind.Parameters.AddWithValue("@BookId", "");
                queryBookFind.Parameters.AddWithValue("@OperationType", "SeeBooks");
                SqlDataReader dataReaderBookFind = queryBookFind.ExecuteReader();

                Console.WriteLine("Enter the  BookId which you want to delete");
                while (dataReaderBookFind.Read())
                {
                    Console.WriteLine("BookId: " + dataReaderBookFind["Id"] + " "+ "BookTitle:" + dataReaderBookFind["Title"] + " " + "Book Edition: " + dataReaderBookFind["Edition"]);
                }
                dataReaderBookFind.Close();

                int BookId = Convert.ToInt32(Console.ReadLine());

                //deleting that BookId bok
                SqlCommand queryBookDelete = new SqlCommand("BookSelectOrDelete", con);
                queryBookDelete.CommandType = System.Data.CommandType.StoredProcedure;
                queryBookDelete.Parameters.AddWithValue("@BookId", BookId);
                queryBookDelete.Parameters.AddWithValue("@OperationType", "Delete");
                 queryBookDelete.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Deleted Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
           
                con.Close();
            }


        }
    }
}
