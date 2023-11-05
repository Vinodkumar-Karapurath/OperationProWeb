using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace AdminWebCore.Pages
{
    public class MasterAddModel : PageModel
    {
        private IConfiguration Config { get; set; }

       // public List<MasterModel> masterModel = null;
        public string Type;

        public MasterAddModel(IConfiguration configuration)
        {
            Config = configuration;

        }

        public void OnGet(string Types)
        {
            Type = Types;
        }

        public void OnPostSubmit(AdminWebCore.Models.MastersAddModel masters)
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            string result = db.MastersSave(masters);
            Type = masters.Type;
        }
    }
}
