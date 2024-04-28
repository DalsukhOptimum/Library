using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Admin
{
    public class UpdateBook:ConnectionLogic
    {
       
        int CategoryId;
        
        public void AdminServiceUpdateBook(int AdminId)
        {
           

            try
            {
                con.Open();
                //showing all the books to the admin for update
                SqlCommand queryBookFind = new SqlCommand("BookSelectOrDelete", con);
                queryBookFind.CommandType = CommandType.StoredProcedure;
                queryBookFind.Parameters.AddWithValue("@BookId", "");
                queryBookFind.Parameters.AddWithValue("@OperationType", "SeeBooks");
                SqlDataReader dataReaderBookFind = queryBookFind.ExecuteReader();

                Console.WriteLine("Enter the  BookId which you want to update");
                while (dataReaderBookFind.Read())
                {
                    Console.WriteLine("BookId: " + dataReaderBookFind["Id"] +" "+ "BookTitle:" + dataReaderBookFind["Title"] + " " + "Book Edition: " + dataReaderBookFind["Edition"]);
                }
                dataReaderBookFind.Close();

                int BookId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter New Title");
                string BookTitle = Console.ReadLine();
                
                Console.WriteLine("Enter New Author");
                string BookTAuthor = Console.ReadLine();
               
                Console.WriteLine("Enter Book Edition");
                float BookEdition = (float) Convert.ToDouble(Console.ReadLine());

                //taking category from user and then check whether it exists or not 
                Console.WriteLine("Enter Category");
                string Bookcategory = Console.ReadLine();

            

                SqlCommand CategoryFind = new SqlCommand("CategoryFind", con);

                CategoryFind.CommandType = System.Data.CommandType.StoredProcedure;
                CategoryFind.Parameters.AddWithValue("@CategoryName", Bookcategory);

                SqlParameter Id = CategoryFind.Parameters.AddWithValue("@result", SqlDbType.Int);
                SqlParameter Count = CategoryFind.Parameters.AddWithValue("@Count", SqlDbType.Int);
                Id.Direction = ParameterDirection.Output;
                CategoryFind.ExecuteNonQuery();


                
                if ((int)Count.Value == 0)
                {
                    Console.WriteLine("there is no category name" + Bookcategory);
                    return;
                }
                else
                {
                    CategoryId = (int)Id.Value;
                }

                 //updating a book
                SqlCommand queryUpdateBook = new SqlCommand("InsertIntoBook", con);
                queryUpdateBook.CommandType = CommandType.StoredProcedure;
                queryUpdateBook.Parameters.AddWithValue("@BookId", BookId);
                queryUpdateBook.Parameters.AddWithValue("@OperationType", "UpdateBook");
                queryUpdateBook.Parameters.AddWithValue("@Title", BookTitle);
                queryUpdateBook.Parameters.AddWithValue("@Author", BookTAuthor);
                queryUpdateBook.Parameters.AddWithValue("@Category", CategoryId);
                queryUpdateBook.Parameters.AddWithValue("@CreatedBy", "");
               
                queryUpdateBook.Parameters.AddWithValue("@UpdatedBy",AdminId );
                queryUpdateBook.Parameters.AddWithValue("@BookCount", "");
                queryUpdateBook.Parameters.AddWithValue("@AvailableBook", "");
                queryUpdateBook.Parameters.AddWithValue("@Edition", BookEdition);
                queryUpdateBook.Parameters.AddWithValue("@BookOwn", "");
                queryUpdateBook.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Updated Succesfully");
            }


            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
            }


        }
    }
}
