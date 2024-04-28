using System.Data;
using System.Data.SqlClient;

namespace Library.Admin
{
    public class AddCategory:ConnectionLogic
    {
       

        
        public void AdminServiceAddBook(int AdminId)
        {
           
            con.Open();
            Console.WriteLine("Enter Category Name You Want to Add");
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



                if ((int)count.Value != 0)
                {
                    Console.WriteLine("category " + CategoryName + " is already exist ");
                    return;
                }



                //adding category to the category table
                SqlCommand queryAddCategory = new SqlCommand("InsertCategory", con);
                queryAddCategory.CommandType = CommandType.StoredProcedure;
                
                    queryAddCategory.Parameters.AddWithValue("@CategoryName", CategoryName);
                    queryAddCategory.Parameters.AddWithValue("@CreatedBy", AdminId);
                    queryAddCategory.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("category added Succesfully");
                
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
              
                con.Close();
            }

        }
    }
}
