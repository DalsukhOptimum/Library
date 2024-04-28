using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Admin
{
    public class AddBook : ConnectionLogic
    {

        int CategoryId;
       


        public void AdminServiceAddBook(int AdminId)
        {

           
            con.Open();


            //taking category from user and then check whether that id is exist in category table or not 
            Console.WriteLine("Enter Name of Category");
            string CategoryName = Console.ReadLine();

            try
            {
                bool flag = false;
                SqlCommand CategoryFind = new SqlCommand("CategoryFind", con);

                CategoryFind.CommandType = System.Data.CommandType.StoredProcedure;
                CategoryFind.Parameters.AddWithValue("@CategoryName", CategoryName);
  
                SqlParameter Id = CategoryFind.Parameters.AddWithValue("@result", SqlDbType.Int);
                SqlParameter count = CategoryFind.Parameters.AddWithValue("@Count", SqlDbType.Int);
                Id.Direction = ParameterDirection.Output;
                count.Direction = ParameterDirection.Output;
             
                
                CategoryFind.ExecuteNonQuery();


                Console.WriteLine(count.Value);
                if ((int)count.Value == 0)
                {
                    Console.WriteLine("there is no category name" + CategoryName);
                    return;
                }
                else
                {
                    CategoryId = (int)Id.Value;
                }

                Console.WriteLine("Enter Name of the Book");
                string BookTitle = Console.ReadLine();

                Console.WriteLine("Enter Author of Book");
                string BookAuthor = Console.ReadLine();

                Console.WriteLine("How Many Copy Of Books You Want To Add");
                string BookCount = Console.ReadLine();

                Console.WriteLine("which Edition Of Book You Want to Add");
                string Edition = Console.ReadLine();





                //inserting book in the book table
                SqlCommand cmd = new SqlCommand("insertintobook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OperationType", "insertbook");
                cmd.Parameters.AddWithValue("@title", BookTitle);
                cmd.Parameters.AddWithValue("@author", BookAuthor);
                cmd.Parameters.AddWithValue("@category", CategoryId);
                cmd.Parameters.AddWithValue("@createdby", AdminId);
                cmd.Parameters.AddWithValue("@UpdatedBy", "");
                cmd.Parameters.AddWithValue("@BookCount", BookCount);
                cmd.Parameters.AddWithValue("@AvailableBook", BookCount);
                cmd.Parameters.AddWithValue("@Edition", Edition);
                cmd.Parameters.AddWithValue("@BookOwn", "");
                cmd.Parameters.AddWithValue("@BookId", "");



                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Book Added SuccesFully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
    
                con.Close();
            }


        }

    }
}
