using System.Data.SqlClient;
using Task.Classes;

namespace Library.User
{

    public class UserReturn:ConnectionLogic
    {
        
        public void ReturnBook(int UserId)
        {

            try
            {
                con.Open();
                int cnt = 0;
              
                    //showing books which user have 
                SqlCommand queryFindBook = new SqlCommand("UserBooks", con);
                queryFindBook.CommandType = System.Data.CommandType.StoredProcedure;
                queryFindBook.Parameters.AddWithValue("@UserId", UserId);
                queryFindBook.Parameters.AddWithValue("@OperationType", "UserBooks");
                SqlDataReader dataReaderBookTable = queryFindBook.ExecuteReader();
                bool flag = false;
                while (dataReaderBookTable.Read())
                {
                    Console.WriteLine("BookId: " + dataReaderBookTable["Bookid"] + " " + "BookTitle:" + dataReaderBookTable["Title"] + "Book Edition:" + dataReaderBookTable["Edition"]);
                    cnt++;
                }
                dataReaderBookTable.Close();
                //if count is zero means user don't have borrowed books 
                if (cnt == 0)
                {
                    Console.WriteLine("You Don't Have Any Books");
                    return;
                }
                Console.WriteLine("Enter Id of Book which you want to return");
                int BookId = Convert.ToInt32(Console.ReadLine());

                //making entry in book table
                SqlCommand queryUpdateBook = new SqlCommand("BorrowHistoryEntry", con);
                queryUpdateBook.CommandType = System.Data.CommandType.StoredProcedure;

                queryUpdateBook.Parameters.AddWithValue("@BookId", BookId);
                queryUpdateBook.Parameters.AddWithValue("@UserId", UserId);
                queryUpdateBook.Parameters.AddWithValue("@OperationType", "ReturnEntryInBook");
      

                queryUpdateBook.ExecuteNonQuery();

                //making entry in bookborrowhistory table
                SqlCommand returnbookcommand = new SqlCommand("BorrowHistoryEntry", con);
                returnbookcommand.CommandType = System.Data.CommandType.StoredProcedure;
                returnbookcommand.Parameters.AddWithValue("@BookId", BookId);
                returnbookcommand.Parameters.AddWithValue("@UserId",UserId );
                returnbookcommand.Parameters.AddWithValue("@OperationType", "ReturnEntryInBookHistory");
                returnbookcommand.ExecuteNonQuery() ;

                con.Close();
                Console.WriteLine("returned Sucessfully");
            }
            catch (Exception ex)
            {
                Error.PrintingError(ex.Message, "UserReturn");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
