using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class ConfirmAndDeleteModel : PageModel
    {
        public string bookCode = "";
        public Books book = new Books();

        public void OnGet()
        {
            bookCode = Request.Query["bookCode"];
            try
            {
                string bookCodetoUpdate = Request.Query["BookCode"];
                string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select * FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{bookCode}'";


                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    book.bookCode = reader.GetString(0);
                    book.bookTitle = reader.GetString(1);
                    book.author = reader.GetString(3);
                    book.category = reader.GetString(2);
                    book.publication = (string)reader["PUBLICATION"];
                    book.publishDate = Convert.ToDateTime(reader["PUBLISH_DATE"]);
                    book.bookEdition = (int)reader["BOOK_EDITION"];

                    book.price = Convert.ToDecimal(reader["PRICE"]);
                    book.rack_num = (string)reader["RACK_NUM"];
                    book.date_arrival = Convert.ToDateTime(reader["DATE_ARRIVAL"]);
                    book.supplier = (string)reader["SUPPLIER"];






                }
                reader.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("sql exception : " + se);
            }
        }

        public void OnPost()
        {
            bookCode = Request.Query["bookCode"];

            try
            {
                string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                Console.WriteLine(bookCode);
                
                cmd.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{bookCode}'";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
                if(rowsAffected > 0)
                {
                    Response.Redirect("/Books/Index");
                }
            }catch(SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
    }
}
