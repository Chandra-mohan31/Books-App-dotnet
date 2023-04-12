using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.Books
{
    public class FailedMessageModel : PageModel
    {
        public static string message = "";
        public void OnGet()
        {
            message = Request.Query["message"];
        }
    }
}
