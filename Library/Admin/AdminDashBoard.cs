using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Library.Admin
{
    public class AdminDashBoard:ConnectionLogic
    {


        int AdminId;
        

      
        
        public void AdminServiceDashBoard(string Email, string Password)
        {
            try
            {
              //checking whether  Admin is aailbale or not 
               
                con.Open();
                SqlCommand queryAdminFind = new SqlCommand("IsAvailable", con);
                queryAdminFind.CommandType = System.Data.CommandType.StoredProcedure;
                queryAdminFind.Parameters.AddWithValue("@Email", Email);
                queryAdminFind.Parameters.AddWithValue("@Password", Password);
                queryAdminFind.Parameters.AddWithValue("@Name", "");
                queryAdminFind.Parameters.AddWithValue("@OperationType", "AdminExist");
                SqlParameter Id = queryAdminFind.Parameters.AddWithValue("@Result", SqlDbType.Int);
                SqlParameter Count = queryAdminFind.Parameters.AddWithValue("@Count", SqlDbType.Int);
                Id.Direction = ParameterDirection.Output;
                Count.Direction = ParameterDirection.Output;
               
                queryAdminFind.ExecuteNonQuery();
                bool flag = false;

               
                
                Console.WriteLine(Count.Value);
                if ( (int)Count.Value == 0)
                {
                    Console.WriteLine("you are not an admin");
                    return;
                }
                else{
                    AdminId = (int)Id.Value;
                }
            
                con.Close();
               

                while(true)
                {
                    Console.WriteLine("1.. Want to add Book\n2..want to add Category\n3..want to Update book\n4..want to Delete book\n5.See All Books\n6.see Borrowed Books by user\n7.Return");
                    string choice = Console.ReadLine();
                   

                    switch (choice)
                    {
                        case "1":
                            //adding a book in book table
                            AddBook addbook = new AddBook();
                            addbook.AdminServiceAddBook(AdminId);
                            break;

                        case "2":
                            //adding a category to category table
                            AddCategory addcategory = new AddCategory();
                            addcategory.AdminServiceAddBook(AdminId);
                            break;
                        case "3":
                            //updating a book
                            UpdateBook updatebook = new UpdateBook();
                            updatebook.AdminServiceUpdateBook(AdminId);
                            break;

                        case "4":
                            //deleting book
                            DeleteBook deleteBook = new DeleteBook();
                            deleteBook.AdminServiceDeleteBook(AdminId);
                            break;

                        case "5":
                            //showing all the books to Admin
                            AvailableBooks availableBooks = new AvailableBooks();
                            availableBooks.showBook();
                            break;

                        case "6":
                            //showing books which user borrowed and name of the book and user
                            BookBorrowShow showbook = new BookBorrowShow();
                            showbook.ShowBorrowBooks();
                            break;

                        case "7":
                            return;

                    }
                }

                


            }

            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                con.Close();
               
            }
        }

    }
}
