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
    public class UserRegister:ConnectionLogic
    {
       
        public bool  UserServiceLogin(string Email,string Password, string name) {

            try
            {
                
                con.Open();
            
                // registering user 
                SqlCommand queryUserRegister = new SqlCommand("IsAvailable", con);
                queryUserRegister.CommandType = System.Data.CommandType.StoredProcedure;

                queryUserRegister.Parameters.AddWithValue("@Email", Email);
                queryUserRegister.Parameters.AddWithValue("@Password", Password);
                queryUserRegister.Parameters.AddWithValue("@Name", name);
                queryUserRegister.Parameters.AddWithValue("@OperationType", "UserRegister");
                SqlParameter Id = queryUserRegister.Parameters.AddWithValue("@Result", SqlDbType.Int);
                SqlParameter count = queryUserRegister.Parameters.AddWithValue("@Count", SqlDbType.Int);
                Id.Direction = ParameterDirection.Output;
                count.Direction = ParameterDirection.Output;
           
                queryUserRegister.ExecuteNonQuery();
                Console.WriteLine(count.Value);
              
                con.Close();
                return true;
            }
            catch(Exception ex)
            {
              
                Console.WriteLine(ex.Message);
                Error.PrintingError(ex.Message, "UserRegister");
                return false;
            }
        }
    }
}
