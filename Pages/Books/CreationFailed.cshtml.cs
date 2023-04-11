using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.Books
{
    
    public class CreationFailedModel : PageModel
    {
        public static string errorMessage = "";
        public void OnGet()
        {
            errorMessage = Request.Query["error"];
        }

        
    }
}
