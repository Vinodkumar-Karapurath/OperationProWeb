using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminWebCore.Pages
{
    public class IndexModel1 : PageModel
    {
        public IActionResult OnGet()
        {
            return new RedirectResult("/Start");
        }
    }
}