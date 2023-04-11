using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

using System.Web;
namespace WebApplication1.Pages.Books
{
    public class CreateBookModel : PageModel
    {
        public Books b = new Books();
        public string errorMessage = "";
        public string successMessage = "";
        public bool hiddenSucc = true;
        public bool hiddenError = true;

        public void OnGet()
        {
        }

        public void OnPost() {

            errorMessage = "";
            successMessage = "";
            try
            {
                
                string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                b.bookCode = Request.Form["bookCode"];
                b.bookTitle = Request.Form["Title"];
                b.author = Request.Form["Author"];
                b.price = Convert.ToDecimal(Request.Form["price"]);
                b.date_arrival = Convert.ToDateTime(Request.Form["date_arrival"]);
                b.publishDate= Convert.ToDateTime(Request.Form["publish_date"]);
                b.bookEdition = Convert.ToInt32(Request.Form["bookEdition"]);
                b.rack_num = Request.Form["rack_num"];
                b.supplier = Request.Form["supplier"];
                b.publication = "my publication";
                b.category = Request.Form["Category"];  

                Console.WriteLine(b.bookEdition);
                SqlCommand cmd = conn.CreateCommand();
                
                cmd.CommandText = $"Insert into LMS_BOOK_DETAILS Values('{b.bookCode}', '{b.bookTitle}', '{b.category}', '{b.author}', '{b.publication}', '{b.publishDate}', {b.bookEdition}, {b.price}, '{b.rack_num}', '{b.date_arrival}', '{b.supplier}');";
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    //RedirectToPage("Books/Index");
                    Console.WriteLine("successfully created..");
                    successMessage = "submitted successfully";
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
                errorMessage = se.Message;
                hiddenError = true;
                Response.Redirect($"/Books/CreationFailed?error={HttpUtility.UrlEncode(errorMessage)}");

            }



        }
    }
}
