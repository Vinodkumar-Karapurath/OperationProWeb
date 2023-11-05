using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AdminWebCore.Pages
{
    public class IndexModel : PageModel
    {
            private IConfiguration Config { get; set; }

        public List<WarningModel> warningModel = null;
        public IndexModel(IConfiguration configuration)
        {
            Config = configuration;

            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            warningModel = db.WarningList();
        }



        //public IActionResult OnGet()
        //{
        //    return new RedirectResult("/index");
        //}
    }
}