using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Admin
{
    public class AvailableBooks:ConnectionLogic
    {
        
        public void showBook ()
        {
            try
            {
                con.Open();
                DataSet dataSet = new DataSet();

                //showing all the books to the user and use dataset and data table
                SqlCommand queryBookFind = new SqlCommand("BookSelectOrDelete", con);
                queryBookFind.CommandType = System.Data.CommandType.StoredProcedure;
                queryBookFind.Parameters.AddWithValue("@OperationType", "SeeBooks");
                queryBookFind.Parameters.AddWithValue("@BookId", "");

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryBookFind);
                sqlDataAdapter.Fill(dataSet);
                
                
              

                int cnt = 0;
                DataTable dataTable = dataSet.Tables[0];
                try
                {
                    //SqlDataReader dataReaderBookFind = queryBookFind.ExecuteReader();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        cnt++;
                        Console.WriteLine("BookId: " + row["Id"] + " " + "BookTitle:" + row["Title"] + " " + "Book Edition:" + row["Edition"]);
                    }

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }


                if (cnt == 0)
                {
                    Console.WriteLine("there is no Books");
                }
        
                con.Close();
          
              
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
         
            }
        }
    }
}
