using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Web;

namespace WebApplication1.Pages.Books
{
    public class UpdateBookModel : PageModel
    {
        public Books book = new Books();
        public string formattedDate_Arrival = "";
        public string formattedDate_Publication = "";
        public string errorMessage = "";
        public void OnGet()
        {

            try
            {
                   string bookCodetoUpdate = Request.Query["BookCode"];
                string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select * FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{bookCodetoUpdate}'";

              
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                   
                    book.bookCode = reader.GetString(0);
                    book.bookTitle = reader.GetString(1);
                    book.author = reader.GetString(3);
                    book.category = reader.GetString(2);
                    book.publication = (string)reader["PUBLICATION"];
                    book.publishDate = Convert.ToDateTime(reader["PUBLISH_DATE"]);
                    formattedDate_Publication = book.publishDate.ToString("yyyy - MM - ddTHH:mm: ss");
                    book.bookEdition = (int)reader["BOOK_EDITION"];
                   
                    book.price = Convert.ToDecimal(reader["PRICE"]);
                    book.rack_num = (string)reader["RACK_NUM"];
                    book.date_arrival = Convert.ToDateTime(reader["DATE_ARRIVAL"]);
                    formattedDate_Arrival = book.date_arrival.ToString("yyyy-MM-ddTHH:mm:ss");
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

            Books b = new Books();
            try
            {

                string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection conn = new SqlConnection(connString);
                string bookCodetoUpdate = Request.Query["BookCode"];

                conn.Open();
                
                b.bookTitle = Request.Form["Title"];
                b.author = Request.Form["Author"];
                b.price = Convert.ToDecimal(Request.Form["price"]);
                b.date_arrival = Convert.ToDateTime(Request.Form["date_arrival"]);
                b.publishDate = Convert.ToDateTime(Request.Form["publish_date"]);
                b.bookEdition = Convert.ToInt32(Request.Form["bookEdition"]);
                b.rack_num = Request.Form["rack_num"];
                b.supplier = Request.Form["supplier"];
                b.publication = "my publication";
                b.category = Request.Form["Category"];

                Console.WriteLine(b.bookEdition);
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = $"Update LMS_BOOK_DETAILS SET BOOK_TITLE = '{b.bookTitle}',CATEGORY='{b.category}',AUTHOR='{b.author}',PUBLISH_DATE='{b.publishDate}',PUBLICATION='{b.publication}',BOOK_EDITION='{b.bookEdition}',PRICE='{b.price}',RACK_NUM='{b.rack_num}',DATE_ARRIVAL='{b.date_arrival}' where BOOK_CODE='{bookCodetoUpdate}' ";
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    //RedirectToPage("Books/Index");
                    Console.WriteLine("successfully created..");
                    Response.Redirect("/Books/CreationSuccess");
                    //hiddenSucc = true;

                }
                else
                {
                    Console.WriteLine("error submitting form");
                    //RedirectToPage("Books/CreationFailed");
                    Response.Redirect("/Books/CreationSuccess");

                }
            }
            catch (SqlException se)
            {
                Console.WriteLine("Error : " + se.Message);
                
                Response.Redirect($"/Books/CreationFailed?error={HttpUtility.UrlEncode(errorMessage)}");

            }



        }
    }
}
