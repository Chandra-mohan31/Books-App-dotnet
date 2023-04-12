using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books

{

    public class Books
    {
        public string bookCode { get; set; }
        public string bookTitle { get; set; }
        public string category { get; set; }
        public string author { get; set; }
        public string publication { get; set; }

        public DateTime publishDate { get; set; }

        public  int bookEdition { get; set;}
        public Decimal price { get; set; }
        public string rack_num { get; set; }

        public DateTime date_arrival { get; set; }  

        public string supplier { get; set; }





    }
    public class IndexModel : PageModel
    {
        public List<Books> bookList = new List<Books>();
        public static int testStatic = 1;
        public void OnGet()
        {
            try
            {
                //string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select BOOK_CODE,BOOK_TITLE,CATEGORY,AUTHOR,PUBLICATION FROM LMS_BOOK_DETAILS";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //for(int i=0;i<reader.FieldCount;i++)
                    //{
                    //    Console.Write($"reader[i] \t");
                    //}
                    //Console.WriteLine();
                    Books book = new Books();
                    book.bookCode = reader.GetString(0);
                    book.bookTitle = reader.GetString(1);
                    book.author = reader.GetString(3);
                    book.category = reader.GetString(2);
                    book.publication = (string)reader["PUBLICATION"];

                    bookList.Add(book);
                }
                reader.Close();
            }catch(SqlException se)
            {
                Console.WriteLine("sql exception : " + se);
            }

        }
        public void OnDeleteButtonClick(string toDeleteBookCode)
        {
            string connString = "Data Source=F48DPF2;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = {toDeleteBookCode}";
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine(rowsAffected);
          
        }
    }
}
